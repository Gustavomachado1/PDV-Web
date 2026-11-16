using Microsoft.EntityFrameworkCore;
using PDV.Application;
using PDV.Domain;

namespace PDV.Infrastructure;

public sealed class CatalogoPadraoSeeder(PdvDbContext db, IUsuarioAtual usuarioAtual)
{
    private static readonly DepartamentoDef[] Catalogo =
    [
        new("Alimentos e Bebidas", "Mercado/Alimentação",
        [
            new("Mercearia Seca", ["Grãos e Cereais", "Massas e Molhos", "Óleos e Temperos", "Farináceos", "Enlatados e Conservas"]),
            new("Doces e Snacks", ["Biscoitos e Bolachas", "Chocolates e Balas", "Salgadinhos e Amendoins", "Matinais"]),
            new("Perecíveis e Frios", ["Laticínios", "Embutidos", "Congelados", "Carnes e Aves"]),
            new("Hortifrúti", ["Frutas", "Verduras", "Legumes", "Ovos"]),
            new("Padaria e Confeitaria", ["Pães", "Bolos e Tortas", "Salgados"]),
            new("Bebidas Não Alcoólicas", ["Águas", "Refrigerantes", "Sucos e Néctares", "Energéticos e Isotônicos", "Cafés e Chás"]),
            new("Bebidas Alcoólicas", ["Cervejas", "Vinhos e Espumantes", "Destilados"])
        ]),
        new("Limpeza e Higiene", "Mercado/Farmácia",
        [
            new("Limpeza Doméstica", ["Limpeza Geral", "Cuidados com Roupas", "Cozinha", "Inseticidas"]),
            new("Higiene Pessoal", ["Cuidados com Cabelos", "Higiene Bucal", "Sabonetes e Desodorantes", "Cuidados com a Pele", "Fraldas e Absorventes"]),
            new("Descartáveis e Embalagens", ["Papéis", "Sacos de Lixo", "Copos e Pratos Descartáveis", "Filmes e Alumínios"])
        ]),
        new("Vestuário e Acessórios", "Moda",
        [
            new("Moda Masculina", ["Camisetas e Camisas", "Calças e Bermudas", "Casacos e Jaquetas", "Moda Íntima e Meias"]),
            new("Moda Feminina", ["Blusas e Camisas", "Vestidos e Saias", "Calças e Shorts", "Moda Íntima", "Moda Praia"]),
            new("Moda Infantil e Bebê", ["Conjuntos", "Bodies e Macacões", "Calçados Infantis"]),
            new("Calçados", ["Tênis", "Sapatos Sociais", "Sandálias e Chinelos", "Botas"]),
            new("Acessórios", ["Bolsas e Mochilas", "Cintos e Carteiras", "Bonés e Chapéus", "Bijuterias e Relógios"])
        ]),
        new("Papelaria, Escritório e Informática", "Papelaria/Tech",
        [
            new("Papelaria Escolar", ["Cadernos e Agendas", "Escrita", "Mochilas e Estojos", "Artes e Pintura"]),
            new("Escritório", ["Papéis", "Organização", "Suprimentos"]),
            new("Informática e Eletrônicos", ["Periféricos", "Cabos e Adaptadores", "Cartuchos e Toners", "Áudio"]),
            new("Acessórios para Celular", ["Capas e Películas", "Carregadores e Power Banks", "Suportes"])
        ]),
        new("Casa, Utilidades e Ferragens", "Bazar/Construção",
        [
            new("Utilidades Domésticas", ["Cozinha e Mesa", "Organização e Lavanderia", "Decoração"]),
            new("Cama, Mesa e Banho", ["Jogos de Lençol", "Toalhas", "Travesseiros e Cobertores"]),
            new("Ferramentas e Manutenção", ["Ferramentas Manuais", "Ferramentas Elétricas", "Elétrica", "Hidráulica", "Pintura e Acessórios"]),
            new("Pet Shop", ["Rações", "Petiscos", "Higiene Pet", "Brinquedos e Acessórios"])
        ]),
        new("Controle Interno", "Geral - Uso Interno/Operacional",
        [
            new("Uso e Consumo", ["Materiais de Loja", "Embalagens de Caixa", "Limpeza Interna"]),
            new("Insumos e Matéria-Prima", ["Produção Própria", "Embalagens Delivery"]),
            new("Serviços", ["Mão de Obra", "Entregas e Frete", "Ajustes e Reparos"])
        ])
    ];

    public async Task<ResultadoSeedCatalogo> PopularAsync(
        Guid empresaId, string? segmento, CancellationToken cancellationToken)
    {
        if (empresaId == Guid.Empty || usuarioAtual.EmpresaId != empresaId)
            throw new InvalidOperationException("O contexto deve corresponder à empresa selecionada para o seed.");

        var catalogoSelecionado = string.IsNullOrWhiteSpace(segmento)
            ? Catalogo
            : Catalogo.Where(x => string.Equals(x.Segmento, segmento.Trim(), StringComparison.OrdinalIgnoreCase)).ToArray();
        if (catalogoSelecionado.Length == 0)
            throw new ArgumentException("O segmento informado não existe no catálogo padrão.", nameof(segmento));

        var departamentosExistentes = await db.Departamentos.ToListAsync(cancellationToken);
        var categoriasExistentes = await db.Categorias.ToListAsync(cancellationToken);
        var subcategoriasExistentes = await db.Subcategorias.ToListAsync(cancellationToken);
        var departamentosPorNome = departamentosExistentes.ToDictionary(x => x.Nome, StringComparer.OrdinalIgnoreCase);
        var categoriasPorChave = categoriasExistentes.ToDictionary(
            x => (x.DepartamentoId, x.Nome), new ChaveCategoriaComparer());
        var subcategoriasPorChave = subcategoriasExistentes.ToDictionary(
            x => (x.CategoriaId, x.Nome), new ChaveCategoriaComparer());
        var adicionados = new ResultadoSeedCatalogo(0, 0, 0);

        foreach (var departamentoDef in catalogoSelecionado)
        {
            if (!departamentosPorNome.TryGetValue(departamentoDef.Nome, out var departamento))
            {
                departamento = new Departamento
                {
                    Nome = departamentoDef.Nome,
                    Segmento = departamentoDef.Segmento
                };
                db.Departamentos.Add(departamento);
                departamentosPorNome.Add(departamento.Nome, departamento);
                adicionados = adicionados with { Departamentos = adicionados.Departamentos + 1 };
            }

            foreach (var categoriaDef in departamentoDef.Categorias)
            {
                var chaveCategoria = (departamento.Id, categoriaDef.Nome);
                if (!categoriasPorChave.TryGetValue(chaveCategoria, out var categoria))
                {
                    categoria = new Categoria
                    {
                        DepartamentoId = departamento.Id,
                        Nome = categoriaDef.Nome
                    };
                    db.Categorias.Add(categoria);
                    categoriasPorChave.Add(chaveCategoria, categoria);
                    adicionados = adicionados with { Categorias = adicionados.Categorias + 1 };
                }

                foreach (var nomeSubcategoria in categoriaDef.Subcategorias)
                {
                    var chaveSubcategoria = (categoria.Id, nomeSubcategoria);
                    if (subcategoriasPorChave.ContainsKey(chaveSubcategoria))
                        continue;

                    var subcategoria = new Subcategoria
                    {
                        CategoriaId = categoria.Id,
                        Nome = nomeSubcategoria
                    };
                    db.Subcategorias.Add(subcategoria);
                    subcategoriasPorChave.Add(chaveSubcategoria, subcategoria);
                    adicionados = adicionados with { Subcategorias = adicionados.Subcategorias + 1 };
                }
            }
        }

        await db.SaveChangesAsync(cancellationToken);
        return adicionados;
    }

    private sealed record DepartamentoDef(string Nome, string Segmento, CategoriaDef[] Categorias);
    private sealed record CategoriaDef(string Nome, string[] Subcategorias);

    private sealed class ChaveCategoriaComparer : IEqualityComparer<(Guid, string)>
    {
        public bool Equals((Guid, string) x, (Guid, string) y)
            => x.Item1 == y.Item1 && StringComparer.OrdinalIgnoreCase.Equals(x.Item2, y.Item2);

        public int GetHashCode((Guid, string) obj)
            => HashCode.Combine(obj.Item1, StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Item2));
    }
}

public sealed record ResultadoSeedCatalogo(int Departamentos, int Categorias, int Subcategorias);
