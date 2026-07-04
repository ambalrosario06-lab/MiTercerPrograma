using MiTercerPrograma.Models;
using MiTercerPrograma.Services;
using Spectre.Console;

class Program
{
    public static bool running = true;
    public static ProductService productService = new();

    public static void Main(string[] args)
    {
        while (running)
        {
            string[] choices =
            {
                "1. Mostrar Productos",
                "2. Eliminar Producto",
                "3. Agregar Producto",
                "4. Salir"
            };

            var selection = new SelectionPrompt<string>()
                .Title("Indica una acción a realizar:")
                .HighlightStyle("darkgoldenrod")
                .AddChoices(choices);

            var option = AnsiConsole.Prompt(selection);

            List<Product> products = productService.FindAll();

            switch (option)
            {
                case "1. Mostrar Productos":

                    AnsiConsole.Clear();

                    LoadingStatus();

                    var table = new Table();

                    table.AddColumn("[Fuchsia]ID[/]");
                    table.AddColumn("[fuchsia]Nombre[/]");
                    table.AddColumn("[fuchsia]Categoría[/]");
                    table.AddColumn("[fuchsia]Precio[/]");

                    foreach (Product product in products)
                    {
                        table.AddRow(
                            product.Id.ToString(),
                            product.Name,
                            product.Category,
                            $"US${product.Price}"
                        );
                    }

                    AnsiConsole.Write(table);
                    break;

                case "2. Eliminar Producto":

                    AnsiConsole.Clear();

                    int id = AnsiConsole.Ask<int>(
                        "Indica el ID del producto a eliminar:"
                    );

                    bool confirmed = AnsiConsole.Confirm("¿Estás seguro?");

                    if (confirmed)
                    {
                        bool isDeleted = productService.Delete(id);

                        if (isDeleted)
                        {
                            AnsiConsole.MarkupLine("[green]Producto eliminado[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]No se encontró el producto.[/]");
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow]Operación cancelada.[/]");
                    }

                    break;

                case "3. Agregar Producto":

                    AnsiConsole.Clear();

                    string name = AnsiConsole.Ask<string>(
                        "Indica el nombre del producto:"
                    );

                    string category = AnsiConsole.Ask<string>(
                        "Indica la categoría del producto:"
                    );

                    double price = AnsiConsole.Ask<double>(
                        "Indica el precio del producto:"
                    );

                    int maxId = products.Any() ? products.Max(c => c.Id) : 0;

                    Product newProduct = new Product
                    {
                        Id = maxId + 1,
                        Name = name,
                        Category = category,
                        Price = price
                    };

                    LoadingStatus();

                    productService.Create(newProduct);

                    AnsiConsole.MarkupLine("[green]Producto agregado correctamente.[/]");

                    break;

                case "4. Salir":

                    AnsiConsole.Clear();

                    AnsiConsole.MarkupLine("[yellow]Fin del programa...[/]");

                    running = false;
                    break;
            }
        }
    }

    public static void LoadingStatus()
    {
        AnsiConsole.Status()
            .Start("Cargando...", ctx =>
            {
                Thread.Sleep(1000);
            });
    }
}
