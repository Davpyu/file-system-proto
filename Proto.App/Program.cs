using Proto.App.Services;
using Proto.App.Models;
using Proto.App.Enums;

// Test.Run();

bool loop = true;
int lastId = 0;

AvlTreeService tree = new();

while (loop)
{
    Console.WriteLine("Menu: ");
    Console.WriteLine("1. Add");
    Console.WriteLine("2. Delete");
    Console.WriteLine("3. Search");
    Console.WriteLine("4. Exit");
    Console.WriteLine("Choose menu: ");
    Menu menu = (Menu) Convert.ToInt32(Console.ReadLine());
    Console.WriteLine($"{menu}");

    switch (menu)
    {
        case Menu.Insert:
            Console.WriteLine("Insert data");
            Console.WriteLine("File Name: ");
            string fileName = Console.ReadLine();
            Console.WriteLine("File Size: ");
            int fileSize = Convert.ToInt32(Console.ReadLine());

            lastId += 1;

            tree._root = tree.InsertData(tree._root, new Metadata() {
                Id = lastId,
                Name = fileName,
                Size = fileSize,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
            });

            Console.Clear();
            Console.WriteLine("Preorder traversal is: ");
            AvlTreeService.PreOrder(tree._root);

            break;
        case Menu.Delete:
            Console.WriteLine("Insert Id that you want to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            tree._root = tree.DeleteData(tree._root, id);

            Console.Clear();
            Console.WriteLine("Preorder traversal is: ");
            AvlTreeService.PreOrder(tree._root);
            break;
        case Menu.Search:
            Console.WriteLine("Insert Id that you want to search: ");
            int search = Convert.ToInt32(Console.ReadLine());
            Metadata data = AvlTreeService.Search(tree._root, search);

            if (data != null)
            {
                Console.Write("Data: ");
                Console.WriteLine(data.Serialize());
            }

            else
            {
                Console.WriteLine("Data Tidak Ditemukan");
            }
            break;
        default:
            loop = false;
            break;
    }
}