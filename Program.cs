using System;
using System.Collections.Generic;

class Livro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int Quantidade { get; set; }
}

class Usuario
{
    public List<Livro> LivrosEmprestados = new List<Livro>();
}

class Biblioteca
{
    public List<Livro> Catalogo = new List<Livro>();

    public void CadastrarLivro(string titulo, string autor, int quantidade)
    {
        Catalogo.Add(new Livro { Titulo = titulo, Autor = autor, Quantidade = quantidade });
    }

    public void ConsultarCatalogo()
    {
        foreach (var livro in Catalogo)
            Console.WriteLine($"{livro.Titulo} - Autor: {livro.Autor} - Disponíveis: {livro.Quantidade}");
    }
}

class Program
{
    static void Main()
    {
        var biblioteca = new Biblioteca();
        var usuario = new Usuario();
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("-----Bem vindo a biblioteca!----- \nMenu:\n1. Cadastrar Livro\n2. Consultar Catálogo\n3. Emprestar Livro\n4. Devolver Livro\n5. Sair");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();
                    Console.Write("Quantidade: ");
                    int quantidade = int.Parse(Console.ReadLine());
                    biblioteca.CadastrarLivro(titulo, autor, quantidade);
                    Console.WriteLine("Livro cadastrado!");
                    break;

                case "2":
                    biblioteca.ConsultarCatalogo();
                    break;

                case "3":
                    if (usuario.LivrosEmprestados.Count >= 3)
                    {
                        Console.WriteLine("Limite de 3 livros atingido!");
                        break;
                    }

                    Console.Write("Digite o título do livro: ");
                    string tituloEmprestimo = Console.ReadLine();
                    var livroEmprestimo = biblioteca.Catalogo.Find(l => l.Titulo.Equals(tituloEmprestimo, StringComparison.OrdinalIgnoreCase));

                    if (livroEmprestimo?.Quantidade > 0)
                    {
                        usuario.LivrosEmprestados.Add(livroEmprestimo);
                        livroEmprestimo.Quantidade--;
                        Console.WriteLine("Livro emprestado!");
                    }
                    else
                    {
                        Console.WriteLine("Livro não disponível.");
                    }
                    break;

                case "4":
                    Console.Write("Digite o título do livro: ");
                    string tituloDevolucao = Console.ReadLine();
                    var livroDevolucao = usuario.LivrosEmprestados.Find(l => l.Titulo.Equals(tituloDevolucao, StringComparison.OrdinalIgnoreCase));

                    if (livroDevolucao != null)
                    {
                        usuario.LivrosEmprestados.Remove(livroDevolucao);
                        livroDevolucao.Quantidade++;
                        Console.WriteLine("Livro devolvido!");
                    }
                    else
                    {
                        Console.WriteLine("Você não tem este livro.");
                    }
                    break;

                case "5":
                    sair = true;
					Console.Write("Até logo!");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
