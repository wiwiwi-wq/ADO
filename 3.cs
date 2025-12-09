////1
////using System;
////using System.Data;

////class Program
////{
////    static void Main()
////    {
////        DataSet dataSet = new DataSet("Магазин");

////        try
////        {
////            DataTable categories = CreateCategoriesTable();
////            DataTable products = CreateProductsTable();

////            dataSet.Tables.Add(categories);
////            dataSet.Tables.Add(products);

////            FillCategories(categories);
////            FillProducts(products);

////            DataRelation relation = CreateCategoryProductRelation(categories, products);
////            dataSet.Relations.Add(relation);

////            DisplayRelationInfo(relation);

////            DisplayHierarchy(dataSet);

////        }
////        catch (Exception ex)
////        {
////            Console.ForegroundColor = ConsoleColor.Red;
////            Console.WriteLine($"Ошибка: {ex.Message}");
////            Console.ResetColor();
////        }

////        Console.WriteLine("\nНажмите любую клавишу для выхода...");
////        Console.ReadKey();
////    }

////    static DataTable CreateCategoriesTable()
////    {
////        DataTable table = new DataTable("Категории");

////        table.Columns.Add("CategoryID", typeof(int));
////        table.Columns.Add("CategoryName", typeof(string));
////        table.Columns.Add("Description", typeof(string));

////        table.PrimaryKey = new DataColumn[] { table.Columns["CategoryID"] };

////        return table;
////    }

////    static DataTable CreateProductsTable()
////    {
////        DataTable table = new DataTable("Товары");

////        table.Columns.Add("ProductID", typeof(int));
////        table.Columns.Add("ProductName", typeof(string));
////        table.Columns.Add("Price", typeof(decimal));
////        table.Columns.Add("CategoryID", typeof(int)); 

////        table.PrimaryKey = new DataColumn[] { table.Columns["ProductID"] };

////        return table;
////    }

////    static void FillCategories(DataTable categories)
////    {
////        categories.Rows.Add(1, "Напитки", "Безалкогольные и алкогольные напитки");
////        categories.Rows.Add(2, "Электроника", "Смартфоны, ноутбуки, аксессуары");
////        categories.Rows.Add(3, "Продукты питания", "Скоропортящиеся и долговечные товары");
////        categories.Rows.Add(4, "Книги", "Художественная и научная литература");
////    }

////    static void FillProducts(DataTable products)
////    {
////        products.Rows.Add(1, "Кола 1л", 85.50m, 1);
////        products.Rows.Add(2, "Сок апельсиновый", 120.00m, 1);
////        products.Rows.Add(3, "Вода минеральная", 45.00m, 1);
////        products.Rows.Add(4, "iPhone 15", 89990.00m, 2);
////        products.Rows.Add(5, "Наушники AirPods", 14990.00m, 2);
////        products.Rows.Add(6, "Молоко 1л", 78.90m, 3);
////        products.Rows.Add(7, "Хлеб бородинский", 65.00m, 3);
////        products.Rows.Add(8, "Сыр российский", 450.00m, 3);
////        products.Rows.Add(9, "Война и мир", 890.00m, 4);
////        products.Rows.Add(10, "C# для профессионалов", 2500.00m, 4);
////    }

////    static DataRelation CreateCategoryProductRelation(DataTable parent, DataTable child)
///    {
////        try
////        {
////            DataColumn parentColumn = parent.Columns["CategoryID"];
////            DataColumn childColumn = child.Columns["CategoryID"];

////            DataRelation relation = new DataRelation(
////                "Категория_Товары",    
////                parentColumn,           
////                childColumn,          
////                createConstraints: true
////            );

////            relation.ChildKeyConstraint.UpdateRule = Rule.Cascade;
////            relation.ChildKeyConstraint.DeleteRule = Rule.Cascade;

////            return relation;
////        }
////        catch (ArgumentException ex)
////        {
////            throw new InvalidOperationException(
////                "Не удалось создать отношение: несоответствие типов колонок или колонка не найдена.", ex);
////        }
////        catch (ConstraintException ex)
////        {
////            throw new InvalidOperationException(
////                "Нарушение ограничений при создании отношения (возможно, дубли или NULL в ключах).", ex);
////        }
////    }

////    static void DisplayRelationInfo(DataRelation relation)
////    {
////        Console.WriteLine("=== Информация об отношении ===");
////        Console.WriteLine($"Имя отношения: {relation.RelationName}");
////        Console.WriteLine($"Родительская таблица: {relation.ParentTable.TableName}");
////        Console.WriteLine($"Дочерняя таблица таблица: {relation.ChildTable.TableName}");
////        Console.WriteLine($"Родительская колонка: {relation.ParentColumns[0].ColumnName}");
////        Console.WriteLine($"Дочерняя колонка: {relation.ChildColumns[0].ColumnName}");
////        Console.WriteLine($"Ограничения созданы: {relation.ParentKeyConstraint != null && relation.ChildKeyConstraint != null}");
////        Console.WriteLine();
////    }

////    static void DisplayHierarchy(DataSet dataSet)
////    {
////        Console.WriteLine("=== Иерархия: Категории → Товары ===\n");

////        DataTable categories = dataSet.Tables["Категории"];

////        foreach (DataRow categoryRow in categories.Rows)
////        {
////            int categoryId = (int)categoryRow["CategoryID"];
////            string categoryName = categoryRow["CategoryName"].ToString();
////            string description = categoryRow["Description"]?.ToString() ?? "";

////            Console.ForegroundColor = ConsoleColor.Cyan;
////            Console.WriteLine($"[{categoryId}] {categoryName}");
////            Console.ResetColor();
////            Console.WriteLine($"    Описание: {description}");
////            Console.WriteLine("    Товары:");

////            DataRow[] productRows = categoryRow.GetChildRows("Категория_Товары");

////            if (productRows.Length == 0)
////            {
////                Console.WriteLine("        (нет товаров)");
////            }
////            else
////            {
////                foreach (DataRow productRow in productRows)
////                {
////                    Console.WriteLine($"        • {productRow["ProductName"]}  |  Цена: {productRow["Price"]:C}  |  ID: {productRow["ProductID"]}");
////                }
////            }
////            Console.WriteLine();
////        }
////    }
////}

////2
//using System;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using static System.Net.Mime.MediaTypeNames;

//class Program
//{
//    static DataSet dataSet;

//    static void Main()
//    {
//        dataSet = new DataSet("Магазин");

//        var categories = new DataTable("Категории");
//        categories.Columns.Add("CategoryID", typeof(int));
//        categories.Columns.Add("CategoryName", typeof(string));
//        categories.Columns.Add("Description", typeof(string));
//        categories.PrimaryKey = new[] { categories.Columns["CategoryID"] };

//        var products = new DataTable("Товары");
//        products.Columns.Add("ProductID", typeof(int));
//        products.Columns.Add("ProductName", typeof(string));
//        products.Columns.Add("Price", typeof(decimal));
//        products.Columns.Add("CategoryID", typeof(int));
//        products.PrimaryKey = new[] { products.Columns["ProductID"] };

//        categories.Rows.Add(1, "Напитки", "Безалкогольные и алкогольные напитки");
//        categories.Rows.Add(2, "Электроника", "Гаджеты и техника");
//        categories.Rows.Add(3, "Продукты питания", "Еда и бакалея");
//        categories.Rows.Add(4, "Книги", "Литература");

//        products.Rows.Add(1, "Кола 1л", 85.50m, 1);
//        products.Rows.Add(2, "Сок апельсиновый", 120.00m, 1);
//        products.Rows.Add(3, "Вода минеральная", 45.00m, 1);
//        products.Rows.Add(4, "iPhone 15", 89990.00m, 2);
//        products.Rows.Add(5, "AirPods Pro", 24990.00m, 2);
//        products.Rows.Add(6, "Молоко", 78.90m, 3);
//        products.Rows.Add(7, "Хлеб", 65.00m, 3);
//        products.Rows.Add(8, "Сыр", 450.00m, 3);
//        products.Rows.Add(9, "Война и мир", 1890.00m, 4);
//        products.Rows.Add(10, "C# 10 и .NET 6", 3200.00m, 4);

//        dataSet.Tables.Add(categories);
//        dataSet.Tables.Add(products);

//        var relation = new DataRelation("Категория_Товары",
//            categories.Columns["CategoryID"],
//            products.Columns["CategoryID"],
//            createConstraints: true);

//        dataSet.Relations.Add(relation);

//        DisplayAllCategoriesWithProducts();
//        Console.WriteLine(new string('═', 60));
//        SearchProductsInCategory(1);
//        SearchProductsInCategory(2);
//        SearchProductsInCategory(999);
//        Console.WriteLine(new string('═', 60));
//        DisplayProductCountPerCategory();
//        Console.WriteLine(new string('═', 60));
//        DisplayTotalPricePerCategory();
//        Console.WriteLine(new string('═', 60));
//        DisplayCategoriesWithExpensiveProducts(10000m);

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void DisplayAllCategoriesWithProducts()
//    {
//        Console.WriteLine("СПИСОК КАТЕГОРИЙ И ТОВАРОВ:\n");

//        foreach (DataRow cat in dataSet.Tables["Категории"].Rows)
//        {
//            Console.ForegroundColor = ConsoleColor.Yellow;
//            Console.WriteLine($"[{cat["CategoryID"]}] {cat["CategoryName"]}");
//            Console.ResetColor();
//            Console.WriteLine(new string('-', 50));

//            DataRow[] childs = cat.GetChildRows("Категория_Товары");

//            if (childs.Length == 0)
//            {
//                Console.WriteLine("    (нет товаров)\n");
//                continue;
//            }

//            foreach (DataRow p in childs)
//                Console.WriteLine($"    • {p["ProductName"],-35}  Цена: {((decimal)p["Price"]):C}");

//            Console.WriteLine();
//        }
//    }

//    static void SearchProductsInCategory(int categoryId)
//    {
//        var cat = dataSet.Tables["Категории"]
//            .Select($"CategoryID = {categoryId}")
//            .FirstOrDefault();

//        if (cat == null)
//        {
//            Console.WriteLine($"Категория с ID={categoryId} не найдена.\n");
//            return;
//        }

//        Console.WriteLine($"Товары в категории \"{cat["CategoryName"]}\":");

//        DataRow[] products = cat.GetChildRows("Категория_Товары");

//        if (products.Length == 0)
//            Console.WriteLine("    Нет товаров\n");
//        else
//            foreach (var p in products)
//                Console.WriteLine($"    → {p["ProductName"]} | {p["Price"]:C}");

//        Console.WriteLine();
//    }

//    static void DisplayProductCountPerCategory()
//    {
//        Console.WriteLine("КОЛИЧЕСТВО ТОВАРОВ ПО КАТЕГОРИЯМ:\n");
//        Console.WriteLine($"{"Категория",-30} {"Товаров",8}");

//        foreach (DataRow cat in dataSet.Tables["Категории"].Rows)
//        {
//            int cnt = cat.GetChildRows("Категория_Товары").Length;
//            Console.WriteLine($"{cat["CategoryName"],-30} {cnt,8}");
//        }
//        Console.WriteLine();
//    }

//    static void DisplayTotalPricePerCategory()
//    {
//        Console.WriteLine("ОБЩАЯ СТОИМОСТЬ ТОВАРОВ В КАТЕГОРИЯХ:\n");
//        Console.WriteLine($"{"Категория",-30} {"Сумма",15}");

//        foreach (DataRow cat in dataSet.Tables["Категории"].Rows)
//        {
//            decimal sum = cat.GetChildRows("Категория_Товары")
//                .Sum(p => (decimal)p["Price"]);

//            Console.WriteLine($"{cat["CategoryName"],-30} {sum,15:C}");
//        }
//        Console.WriteLine();
//    }

//    static void DisplayCategoriesWithExpensiveProducts(decimal minPrice)
//    {
//        Console.WriteLine($"КАТЕГОРИИ С ТОВАРАМИ ДОРОЖЕ {minPrice:C}:\n");

//        bool found = false;

//        foreach (DataRow cat in dataSet.Tables["Категории"].Rows)
//        {
//            var expensive = cat.GetChildRows("Категория_Товары")
//                .Where(p => (decimal)p["Price"] > minPrice)
//                .ToArray();

//            if (expensive.Length > 0)
//            {
//                found = true;
//                Console.ForegroundColor = ConsoleColor.Green;
//                Console.WriteLine($"[{cat["CategoryID"]}] {cat["CategoryName"]}");
//                Console.ResetColor();

//                foreach (var p in expensive)
//                    Console.WriteLine($"    • {p["ProductName"]} — {p["Price"]:C}");

//                Console.WriteLine();
//            }
//        }

//        if (!found)
//            Console.WriteLine("    Нет таких категорий.\n");
//    }
//}

//3
//using System;
//using System.Data;
//using System.Linq;

//class Program
//{
//    static DataSet dataSet;

//    static void Main()
//    {
//        dataSet = new DataSet();

//        var categories = new DataTable("Категории");
//        categories.Columns.Add("CategoryID", typeof(int));
//        categories.Columns.Add("CategoryName", typeof(string));
//        categories.Columns.Add("Description", typeof(string));
//        categories.PrimaryKey = new[] { categories.Columns["CategoryID"] };

//        var products = new DataTable("Товары");
//        products.Columns.Add("ProductID", typeof(int));
//        products.Columns.Add("ProductName", typeof(string));
//        products.Columns.Add("Price", typeof(decimal));
//        products.Columns.Add("CategoryID", typeof(int));
//        products.PrimaryKey = new[] { products.Columns["ProductID"] };

//        categories.Rows.Add(1, "Напитки", "Безалкогольные и алкогольные напитки");
//        categories.Rows.Add(2, "Электроника", "Гаджеты и аксессуары");
//        categories.Rows.Add(3, "Продукты питания", "Съедобное");

//        products.Rows.Add(1, "Кола 1л", 85.50m, 1);
//        products.Rows.Add(2, "iPhone 15", 89990m, 2);
//        products.Rows.Add(3, "Сыр", 450m, 3);
//        products.Rows.Add(4, "Осиротевший товар", 999m, 99);
//        products.Rows.Add(5, "Вода", 45m, 1);

//        dataSet.Tables.Add(categories);
//        dataSet.Tables.Add(products);

//        var relation = new DataRelation("Категория_Товары",
//            categories.Columns["CategoryID"],
//            products.Columns["CategoryID"],
//            createConstraints: true);
//        dataSet.Relations.Add(relation);

//        Console.WriteLine("РАБОТА С GETPARENTROWS()\n");

//        DisplayProductWithCategory(1);
//        DisplayProductWithCategory(2);
//        DisplayProductWithCategory(4);
//        DisplayProductWithCategory(999);

//        Console.WriteLine(new string('═', 70));

//        FullProductReport();

//        Console.WriteLine(new string('═', 70));

//        DetectOrphanedProducts();

//        Console.WriteLine(new string('═', 70));

//        DemonstrateDataRowVersion();

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void DisplayProductWithCategory(int productId)
//    {
//        var product = dataSet.Tables["Товары"].Rows.Find(productId);
//        if (product == null)
//        {
//            Console.WriteLine($"Товар с ID={productId} не найден.");
//            return;
//        }

//        DataRow[] parents = product.GetParentRows("Категория_Товары");

//        Console.WriteLine($"Товар: {product["ProductName"]} (ID: {productId})");

//        if (parents.Length == 0)
//            Console.WriteLine("    Категория: <отсутствует или удалена>");
//        else
//            Console.WriteLine($"    Категория: {parents[0]["CategoryName"]} (ID: {parents[0]["CategoryID"]})");

//        Console.WriteLine();
//    }

//    static void FullProductReport()
//    {
//        Console.WriteLine("ОТЧЁТ: ТОВАР → КАТЕГОРИЯ → ОПИСАНИЕ КАТЕГОРИИ\n");
//        Console.WriteLine($"{"ID товара",-10} {"Товар",-35} {"Категория",-20} {"Описание категории"}");
//        Console.WriteLine(new string('-', 100));

//        foreach (DataRow prod in dataSet.Tables["Товары"].Rows)
//        {
//            DataRow[] parents = prod.GetParentRows("Категория_Товары");

//            string catName = parents.Length > 0 ? parents[0]["CategoryName"].ToString() : "<нет категории>";
//            string desc = parents.Length > 0 ? parents[0]["Description"].ToString() : "";

//            Console.WriteLine($"{prod["ProductID"],-10} {prod["ProductName"],-35} {catName,-20} {desc}");
//        }
//    }

//    static void DetectOrphanedProducts()
//    {
//        Console.WriteLine("ТОВАРЫ БЕЗ КАТЕГОРИИ (нарушение ссылочной целостности):\n");

//        var orphans = dataSet.Tables["Товары"].AsEnumerable()
//            .Where(p => p.GetParentRows("Категория_Товары").Length == 0);

//        if (!orphans.Any())
//            Console.WriteLine("    Нарушений не обнаружено.");
//        else
//            foreach (var p in orphans)
//                Console.WriteLine($"    ID {p["ProductID"]} — {p["ProductName"]} (CategoryID={p["CategoryID"]})");
//    }

//    static void DemonstrateDataRowVersion()
//    {
//        Console.WriteLine("ДЕМОНСТРАЦИЯ GETPARENTROWS() С DATAROWVERSION\n");

//        var testProduct = dataSet.Tables["Товары"].Rows.Find(5);
//        testProduct["CategoryID"] = 2;
//        testProduct["Price"] = 50m;

//        DataRow[] current = testProduct.GetParentRows("Категория_Товары", DataRowVersion.Current);
//        DataRow[] original = testProduct.GetParentRows("Категория_Товары", DataRowVersion.Original);

//        Console.WriteLine("Товар ID=5 изменён: CategoryID с 1 → 2");

//        Console.WriteLine("GetParentRows(Current)  → " +
//            (current.Length > 0 ? current[0]["CategoryName"] : "<нет>"));

//        Console.WriteLine("GetParentRows(Original) → " +
//            (original.Length > 0 ? original[0]["CategoryName"] : "<нет>"));

//        testProduct.Delete();

//        try
//        {
//            DataRow[] deleted = testProduct.GetParentRows("Категория_Товары", DataRowVersion.Current);
//            Console.WriteLine("После Delete() — Current: " + (deleted.Length > 0 ? deleted[0]["CategoryName"] : "<недоступно>"));
//        }
//        catch (DeletedRowInaccessibleException)
//        {
//            Console.WriteLine("После Delete() — Current: <ошибка — строка удалена>");
//        }

//        DataRow[] origAfterDelete = testProduct.GetParentRows("Категория_Товары", DataRowVersion.Original);
//        Console.WriteLine("После Delete() — Original: " +
//            (origAfterDelete.Length > 0 ? origAfterDelete[0]["CategoryName"] : "<нет>"));
//    }
//}

//4
//using System;
//using System.Data;
//using System.Collections.Generic;

//class Program
//{
//    static DataSet dataSet;
//    static DataTable employees;
//    static DataRelation managerRelation;

//    static void Main()
//    {
//        dataSet = new DataSet();

//        employees = new DataTable("Сотрудники");
//        employees.Columns.Add("EmployeeID", typeof(int));
//        employees.Columns.Add("EmployeeName", typeof(string));
//        employees.Columns.Add("Department", typeof(string));
//        employees.Columns.Add("Salary", typeof(decimal));
//        employees.Columns.Add("ManagerID", typeof(int), "NULL");
//        employees.PrimaryKey = new[] { employees.Columns["EmployeeID"] };

//        dataSet.Tables.Add(employees);

//        managerRelation = new DataRelation("Руководитель_Подчинённые",
//            employees.Columns["EmployeeID"],
//            employees.Columns["ManagerID"],
//            createConstraints: false);

//        dataSet.Relations.Add(managerRelation);

//        FillEmployees();

//        Console.WriteLine("ИЕРАРХИЯ СОТРУДНИКОВ\n");
//        DisplayHierarchy();

//        Console.WriteLine(new string('═', 70));
//        Console.WriteLine("ИНФОРМАЦИЯ ПО КАЖДОМУ СОТРУДНИКУ\n");
//        DisplayEmployeeDetails();

//        Console.WriteLine(new string('═', 70));
//        Console.WriteLine("ГЛУБИНА ИЕРАРХИИ\n");
//        DisplayHierarchyWithLevel();

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void FillEmployees()
//    {
//        employees.Rows.Add(1, "Иванов Иван Иванович", "Генеральный директор", 500000m, DBNull.Value);
//        employees.Rows.Add(2, "Петров Пётр Петрович", "Финансовый отдел", 250000m, 1);
//        employees.Rows.Add(3, "Сидорова Анна Сергеевна", "IT-отдел", 300000m, 1);
//        employees.Rows.Add(4, "Козлов Алексей Викторович", "Бухгалтерия", 180000m, 2);
//        employees.Rows.Add(5, "Васильева Мария Ивановна", "Программист", 220000m, 3);
//        employees.Rows.Add(6, "Михайлов Дмитрий Олегович", "Программист", 210000m, 3);
//        employees.Rows.Add(7, "Новикова Екатерина Андреевна", "HR-отдел", 190000m, 1);
//        employees.Rows.Add(8, "Фёдоров Сергей Павлович", "Стажёр", 80000m, 5);
//    }

//    static void DisplayHierarchy()
//    {
//        var roots = employees.Select("ManagerID IS NULL");

//        foreach (DataRow root in roots)
//            PrintEmployee(root, 0);
//    }

//    static void PrintEmployee(DataRow emp, int level)
//    {
//        string indent = new string(' ', level * 4);
//        Console.WriteLine($"{indent}{emp["EmployeeName"]} — {emp["Department"]} ({emp["Salary"]:C}/мес)");

//        DataRow[] children = emp.GetChildRows(managerRelation);

//        foreach (DataRow child in children)
//            PrintEmployee(child, level + 1);
//    }

//    static void DisplayEmployeeDetails()
//    {
//        foreach (DataRow emp in employees.Rows)
//        {
//            int id = (int)emp["EmployeeID"];
//            string name = emp["EmployeeName"].ToString();

//            DataRow[] parents = emp.GetParentRows(managerRelation);
//            string manager = parents.Length > 0 ? parents[0]["EmployeeName"].ToString() : "— (глава компании)";

//            DataRow[] children = emp.GetChildRows(managerRelation);
//            string subordinates = children.Length > 0
//                ? string.Join(", ", Array.ConvertAll(children, c => c["EmployeeName"].ToString()))
//                : "нет подчинённых";

//            Console.WriteLine($"Сотрудник: {name} (ID: {id})");
//            Console.WriteLine($"   Руководитель: {manager}");
//            Console.WriteLine($"   Подчинённые: {subordinates}");
//            Console.WriteLine();
//        }
//    }

//    static void DisplayHierarchyWithLevel()
//    {
//        var roots = employees.Select("ManagerID IS NULL");

//        foreach (DataRow root in roots)
//            PrintEmployeeWithLevel(root, 0);
//    }

//    static void PrintEmployeeWithLevel(DataRow emp, int level)
//    {
//        string indent = new string('│   ', level);
//        string prefix = level == 0 ? "" : "└── ";

//        Console.WriteLine($"{indent}{prefix}[Уровень {level}] {emp["EmployeeName"]} — {emp["Department"]}");

//        DataRow[] children = emp.GetChildRows(managerRelation);

//        for (int i = 0; i < children.Length; i++)
//        {
//            bool isLast = i == children.Length - 1;
//            string childIndent = indent + (isLast ? "    " : "│   ");
//            string childPrefix = isLast ? "└── " : "├── ";

//            Console.Write($"{childIndent}{childPrefix}");
//            PrintEmployeeWithLevel(children[i], level + 1);
//        }
//    }
//}

//5
//using System;
//using System.Data;
//using System.Collections.Generic;
//using System.Linq;

//class Program
//{
//    static DataSet dataSet;
//    static DataTable employees;
//    static DataRelation managerRelation;

//    static void Main()
//    {
//        dataSet = new DataSet();
//        employees = new DataTable("Сотрудники");

//        employees.Columns.Add("EmployeeID", typeof(int));
//        employees.Columns.Add("EmployeeName", typeof(string));
//        employees.Columns.Add("Department", typeof(string));
//        employees.Columns.Add("Salary", typeof(decimal));
//        employees.Columns.Add("ManagerID", typeof(int)).AllowDBNull = true;

//        employees.PrimaryKey = new[] { employees.Columns["EmployeeID"] };
//        dataSet.Tables.Add(employees);

//        managerRelation = new DataRelation("Руководитель_Подчинённые",
//            employees.Columns["EmployeeID"],
//            employees.Columns["ManagerID"],
//            createConstraints: false);

//        dataSet.Relations.Add(managerRelation);

//        FillData();

//        Console.WriteLine("РАСШИРЕННАЯ РАБОТА С ИЕРАРХИЕЙ СОТРУДНИКОВ\n");

//        DirectSubordinates(1);
//        Console.WriteLine(new string('═', 70));

//        ManagementChain(8);
//        ManagementChain(5);
//        Console.WriteLine(new string('═', 70));

//        EmployeesByLevel(2);
//        Console.WriteLine(new string('═', 70));

//        PrintHierarchyStatistics();
//        Console.WriteLine(new string('═', 70));

//        Colleagues(5);
//        Colleagues(1);
//        Console.WriteLine(new string('═', 70));

//        AllManagersAbove(8);

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void FillData()
//    {
//        employees.Rows.Add(1, "Иванов И.И.", "Генеральный директор", 500000m, DBNull.Value);
//        employees.Rows.Add(2, "Петров П.П.", "Финансовый директор", 350000m, 1);
//        employees.Rows.Add(3, "Сидорова А.С.", "Технический директор", 380000m, 1);
//        employees.Rows.Add(4, "Козлова Е.В.", "Главный бухгалтер", 220000m, 2);
//        employees.Rows.Add(5, "Васильев Д.О.", "Руководитель IT-отдела", 280000m, 3);
//        employees.Rows.Add(6, "Михайлова О.Н.", "Ведущий разработчик", 240000m, 5);
//        employees.Rows.Add(7, "Фёдоров С.П.", "Разработчик", 190000m, 5);
//        employees.Rows.Add(8, "Новиков А.В.", "Стажёр", 80000m, 6);
//        employees.Rows.Add(9, "Кузнецова М.А.", "HR-менеджер", 200000m, 1);
//    }

//    static void DirectSubordinates(int managerId)
//    {
//        var manager = employees.Rows.Find(managerId);
//        if (manager == null)
//        {
//            Console.WriteLine($"Менеджер с ID={managerId} не найден.\n");
//            return;
//        }

//        Console.WriteLine($"ПРЯМЫЕ ПОДЧИНЁННЫЕ сотрудника \"{manager["EmployeeName"]}\":\n");
//        var subs = manager.GetChildRows(managerRelation);

//        if (subs.Length == 0)
//            Console.WriteLine("    Нет прямых подчинённых.\n");
//        else
//            foreach (DataRow s in subs)
//                Console.WriteLine($"    • {s["EmployeeName"]} — {s["Department"]}");
//        Console.WriteLine();
//    }

//    static void ManagementChain(int employeeId)
//    {
//        var emp = employees.Rows.Find(employeeId);
//        if (emp == null)
//        {
//            Console.WriteLine($"Сотрудник с ID={employeeId} не найден.\n");
//            return;
//        }

//        Console.WriteLine($"ЦЕПОЧКА РУКОВОДСТВА для \"{emp["EmployeeName"]}\":\n");

//        List<string> chain = new List<string>();
//        DataRow current = emp;

//        while (current != null)
//        {
//            chain.Add(current["EmployeeName"].ToString());
//            DataRow[] parents = current.GetParentRows(managerRelation);
//            current = parents.Length > 0 ? parents[0] : null;
//        }

//        chain.Reverse();
//        for (int i = 0; i < chain.Count; i++)
//            Console.WriteLine($"    {new string(' ', i * 3)}↑ {chain[i]}");

//        Console.WriteLine();
//    }

//    static void EmployeesByLevel(int level)
//    {
//        Console.WriteLine($"СОТРУДНИКИ НА УРОВНЕ {level} (0 = глава компании):\n");

//        var roots = employees.Select("ManagerID IS NULL");
//        var result = new List<DataRow>();

//        CollectByLevel(roots, 0, level, result);

//        if (result.Count == 0)
//            Console.WriteLine("    Нет сотрудников на этом уровне.\n");
//        else
//            foreach (var e in result)
//                Console.WriteLine($"    • {e["EmployeeName"]} — {e["Department"]}");

//        Console.WriteLine();
//    }

//    static void CollectByLevel(DataRow[] nodes, int currentLevel, int targetLevel, List<DataRow> result)
//    {
//        if (currentLevel == targetLevel)
//        {
//            result.AddRange(nodes);
//            return;
//        }

//        foreach (DataRow node in nodes)
//        {
//            var children = node.GetChildRows(managerRelation);
//            CollectByLevel(children, currentLevel + 1, targetLevel, result);
//        }
//    }

//    static void PrintHierarchyStatistics()
//    {
//        Console.WriteLine("СТАТИСТИКА ИЕРАРХИИ\n");

//        var levelDict = new Dictionary<int, int>();
//        var roots = employees.Select("ManagerID IS NULL");

//        CalculateLevels(roots, 0, levelDict);

//        int maxLevel = levelDict.Keys.Max();
//        Console.WriteLine($"Общее количество уровней: {maxLevel + 1}");
//        Console.WriteLine($"Распределение по уровням:");

//        for (int i = 0; i <= maxLevel; i++)
//            Console.WriteLine($"    Уровень {i}: {levelDict[i]} сотрудник(а)");

//        double avgSubordinates = employees.AsEnumerable()
//            .Average(e => e.GetChildRows(managerRelation).Length);

//        Console.WriteLine($"\nСреднее количество прямых подчинённых: {avgSubordinates:F1}");
//        Console.WriteLine();
//    }

//    static void CalculateLevels(DataRow[] nodes, int level, Dictionary<int, int> dict)
//    {
//        if (nodes.Length == 0) return;

//        dict[level] = dict.ContainsKey(level) ? dict[level] + nodes.Length : nodes.Length;

//        foreach (DataRow node in nodes)
//        {
//            var children = node.GetChildRows(managerRelation);
//            CalculateLevels(children, level + 1, dict);
//        }
//    }

//    static void Colleagues(int employeeId)
//    {
//        var emp = employees.Rows.Find(employeeId);
//        if (emp == null)
//        {
//            Console.WriteLine($"Сотрудник с ID={employeeId} не найден.\n");
//            return;
//        }

//        DataRow[] parents = emp.GetParentRows(managerRelation);
//        if (parents.Length == 0)
//        {
//            Console.WriteLine($"У \"{emp["EmployeeName"]}\" нет руководителя → нет коллег.\n");
//            return;
//        }

//        var manager = parents[0];
//        var allSubs = manager.GetChildRows(managerRelation);
//        var colleagues = allSubs.Where(s => (int)s["EmployeeID"] != employeeId);

//        Console.WriteLine($"КОЛЛЕГИ сотрудника \"{emp["EmployeeName"]}\" (один руководитель):\n");

//        if (!colleagues.Any())
//            Console.WriteLine("    Коллег нет (единственный подчинённый руководителя).\n");
//        else
//            foreach (var c in colleagues)
//                Console.WriteLine($"    • {c["EmployeeName"]} — {c["Department"]}");

//        Console.WriteLine();
//    }

//    static void AllManagersAbove(int employeeId)
//    {
//        var emp = employees.Rows.Find(employeeId);
//        if (emp == null)
//        {
//            Console.WriteLine($"Сотрудник с ID={employeeId} не найден.\n");
//            return;
//        }

//        Console.WriteLine($"ВСЕ РУКОВОДИТЕЛИ НАД \"{emp["EmployeeName"]}\":\n");

//        DataRow current = emp;
//        int depth = 0;

//        while (true)
//        {
//            DataRow[] parents = current.GetParentRows(managerRelation);
//            if (parents.Length == 0) break;

//            var manager = parents[0];
//            Console.WriteLine($"    {depth + 1}. {manager["EmployeeName"]} — {manager["Department"]}");
//            current = manager;
//            depth++;
//        }

//        if (depth == 0)
//            Console.WriteLine("    Нет вышестоящих руководителей (глава компании).\n");
//        else
//            Console.WriteLine();
//    }
//}

//6
//using System;
//using System.Data;
//using System.Linq;

//class Program
//{
//    static DataSet dataSet;

//    static void Main()
//    {
//        dataSet = new DataSet("Университет");

//        var students = new DataTable("Студенты");
//        students.Columns.Add("StudentID", typeof(int));
//        students.Columns.Add("StudentName", typeof(string));
//        students.Columns.Add("Email", typeof(string));
//        students.PrimaryKey = new[] { students.Columns["StudentID"] };

//        var courses = new DataTable("Курсы");
//        courses.Columns.Add("CourseID", typeof(int));
//        courses.Columns.Add("CourseName", typeof(string));
//        courses.Columns.Add("Instructor", typeof(string));
//        courses.PrimaryKey = new[] { courses.Columns["CourseID"] };

//        var registrations = new DataTable("Регистрация");
//        registrations.Columns.Add("RegistrationID", typeof(int));
//        registrations.Columns.Add("StudentID", typeof(int));
//        registrations.Columns.Add("CourseID", typeof(int));
//        registrations.Columns.Add("EnrollmentDate", typeof(DateTime));
//        registrations.Columns.Add("Grade", typeof(string));
//        registrations.PrimaryKey = new[] { registrations.Columns["RegistrationID"] };

//        dataSet.Tables.Add(students);
//        dataSet.Tables.Add(courses);
//        dataSet.Tables.Add(registrations);

//        var relStudentReg = new DataRelation("Студент_Регистрации",
//            students.Columns["StudentID"],
//            registrations.Columns["StudentID"],
//            createConstraints: false);

//        var relCourseReg = new DataRelation("Курс_Регистрации",
//            courses.Columns["CourseID"],
//            registrations.Columns["CourseID"],
//            createConstraints: false);

//        dataSet.Relations.Add(relStudentReg);
//        dataSet.Relations.Add(relCourseReg);

//        FillData();

//        Console.WriteLine("СИСТЕМА МНОГИЕ-КО-МНОГИМ: СТУДЕНТЫ ↔ КУРСЫ\n");

//        CoursesOfStudent(1);
//        CoursesOfStudent(3);
//        Console.WriteLine(new string('═', 80));

//        StudentsOnCourse(101);
//        StudentsOnCourse(103);
//        Console.WriteLine(new string('═', 80));

//        PrintEnrollmentMatrix();

//        Console.WriteLine(new string('═', 80));
//        PrintStatistics();

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static DataTable courses;

//    static void FillData()
//    {
//        students.Rows.Add(1, "Иванов Иван", "ivanov@university.ru");
//        students.Rows.Add(2, "Петрова Анна", "petrova@university.ru");
//        students.Rows.Add(3, "Сидоров Пётр", "sidorov@university.ru");
//        students.Rows.Add(4, "Козлова Мария", "kozlova@university.ru");
//        students.Rows.Add(5, "Михайлов Алексей", "mikhailov@university.ru");

//        courses.Rows.Add(101, "Математический анализ", "Проф. Смирнов");
//        courses.Rows.Add(102, "Программирование на C#", "Доц. Кузнецова");
//        courses.Rows.Add(103, "Базы данных", "Проф. Волков");
//        courses.Rows.Add(104, "Физика", "Доц. Орлова");

//        registrations.Rows.Add(1, 1, 101, new DateTime(2025, 9, 1), "5");
//        registrations.Rows.Add(2, 1, 102, new DateTime(2025, 9, 1), "4");
//        registrations.Rows.Add(3, 2, 101, new DateTime(2025, 9, 2), "5");
//        registrations.Rows.Add(4, 2, 103, new DateTime(2025, 9, 2), null);
//        registrations.Rows.Add(5, 3, 102, new DateTime(2025, 9, 3), "5");
//        registrations.Rows.Add(6, 3, 103, new DateTime(2025, 9, 3), "4");
//        registrations.Rows.Add(7, 4, 101, new DateTime(2025, 9, 4), null);
//        registrations.Rows.Add(8, 5, 104, new DateTime(2025, 9, 5), "3");
//    }

//    static void CoursesOfStudent(int studentId)
//    {
//        var student = dataSet.Tables["Студенты"].Rows.Find(studentId);
//        if (student == null)
//        {
//            Console.WriteLine($"Студент с ID={studentId} не найден.\n");
//            return;
//        }

//        Console.WriteLine($"КУРСЫ студента \"{student["StudentName"]}\":\n");

//        var regRows = student.GetChildRows("Студент_Регистрации");
//        if (regRows.Length == 0)
//        {
//            Console.WriteLine("    Не записан ни на один курс.\n");
//            return;
//        }

//        foreach (var reg in regRows)
//        {
//            var courseRow = reg.GetParentRow("Курс_Регистрации");
//            string grade = reg["Grade"] == DBNull.Value ? "не выставлена" : reg["Grade"].ToString();
//            Console.WriteLine($"    • {courseRow["CourseName"]}  |  Преподаватель: {courseRow["Instructor"]}  |  Оценка: {grade}");
//        }
//        Console.WriteLine();
//    }

//    static void StudentsOnCourse(int courseId)
//    {
//        var course = dataSet.Tables["Курсы"].Rows.Find(courseId);
//        if (course == null)
//        {
//            Console.WriteLine($"Курс с ID={courseId} не найден.\n");
//            return;
//        }

//        Console.WriteLine($"СТУДЕНТЫ на курсе \"{course["CourseName"]}\":\n");

//        var regRows = course.GetChildRows("Курс_Регистрации");
//        if (regRows.Length == 0)
//        {
//            Console.WriteLine("    На курсе нет студентов.\n");
//            return;
//        }

//        foreach (var reg in regRows)
//        {
//            var studentRow = reg.GetParentRow("Студент_Регистрации");
//            string grade = reg["Grade"] == DBNull.Value ? "—" : reg["Grade"].ToString();
//            Console.WriteLine($"    • {studentRow["StudentName"]} ({studentRow["Email"]})  → Оценка: {grade}");
//        }
//        Console.WriteLine();
//    }

//    static void PrintEnrollmentMatrix()
//    {
//        Console.WriteLine("МАТРИЦА ЗАПИСЕЙ (Студенты × Курсы)\n");
//        Console.Write("".PadRight(25));
//        foreach (DataRow c in dataSet.Tables["Курсы"].Rows)
//            Console.Write($"{c["CourseName"]}".PadRight(25));
//        Console.WriteLine("\n" + new string('─', 25 + 25 * dataSet.Tables["Курсы"].Rows.Count));

//        foreach (DataRow s in dataSet.Tables["Студенты"].Rows)
//        {
//            Console.Write($"{s["StudentName"]}".PadRight(25));

//            foreach (DataRow c in dataSet.Tables["Курсы"].Rows)
//            {
//                var reg = s.GetChildRows("Студент_Регистрации")
//                           .FirstOrDefault(r => (int)r["CourseID"] == (int)c["CourseID"]);

//                string mark = reg != null
//                    ? (reg["Grade"] == DBNull.Value ? "✓" : reg["Grade"].ToString())
//                    : " ";
//                Console.Write(mark.PadRight(25));
//            }
//            Console.WriteLine();
//        }
//        Console.WriteLine();
//    }

//    static void PrintStatistics()
//    {
//        Console.WriteLine("СТАТИСТИКА ПО КУРСАМ И СТУДЕНТАМ\n");

//        Console.WriteLine("Количество студентов на курсах:");
//        foreach (DataRow c in dataSet.Tables["Курсы"].Rows)
//        {
//            int count = c.GetChildRows("Курс_Регистрации").Length;
//            Console.WriteLine($"    {c["CourseName"]}".PadRight(30) + $" — {count} студент(ов)");
//        }

//        Console.WriteLine("\nКоличество курсов у студентов:");
//        foreach (DataRow s in dataSet.Tables["Студенты"].Rows)
//        {
//            int count = s.GetChildRows("Студент_Регистрации").Length;
//            Console.WriteLine($"    {s["StudentName"]}".PadRight(30) + $" — {count} курс(а)");
//        }
//    }
//}

//7
//using System;
//using System.Data;
//using System.Linq;
//using System.Collections.Generic;

//class Program
//{
//    static DataSet dataSet;
//    static DataTable students, courses, registrations;
//    static DataRelation relStudentReg, relCourseReg;

//    static void Main()
//    {
//        dataSet = new DataSet();

//        students = new DataTable("Студенты");
//        students.Columns.Add("StudentID", typeof(int));
//        students.Columns.Add("StudentName", typeof(string));
//        students.Columns.Add("Email", typeof(string));
//        students.PrimaryKey = new[] { students.Columns["StudentID"] };

//        courses = new DataTable("Курсы");
//        courses.Columns.Add("CourseID", typeof(int));
//        courses.Columns.Add("CourseName", typeof(string));
//        courses.Columns.Add("Instructor", typeof(string));
//        courses.PrimaryKey = new[] { courses.Columns["CourseID"] };

//        registrations = new DataTable("Регистрация");
//        registrations.Columns.Add("RegistrationID", typeof(int));
//        registrations.Columns.Add("StudentID", typeof(int));
//        registrations.Columns.Add("CourseID", typeof(int));
//        registrations.Columns.Add("EnrollmentDate", typeof(DateTime));
//        registrations.Columns.Add("Grade", typeof(int)).AllowDBNull = true;
//        registrations.PrimaryKey = new[] { registrations.Columns["RegistrationID"] };

//        dataSet.Tables.Add(students);
//        dataSet.Tables.Add(courses);
//        dataSet.Tables.Add(registrations);

//        relStudentReg = new DataRelation("Студент_Регистрации",
//            students.Columns["StudentID"], registrations.Columns["StudentID"], false);
//        relCourseReg = new DataRelation("Курс_Регистрации",
//            courses.Columns["CourseID"], registrations.Columns["CourseID"], false);

//        dataSet.Relations.Add(relStudentReg);
//        dataSet.Relations.Add(relCourseReg);

//        FillData();

//        Console.WriteLine("МНОГИЕ-КО-МНОГИМ: ПОЛНАЯ НАВИГАЦИЯ И АНАЛИТИКА\n");

//        StudentFullCoursesAndGrades(1);
//        StudentFullCoursesAndGrades(3);
//        Console.WriteLine(new string('═', 90));

//        CourseFullStudentsAndGrades(102);
//        CourseFullStudentsAndGrades(103);
//        Console.WriteLine(new string('═', 90));

//        FindStudentsWithCommonCourses();
//        Console.WriteLine(new string('═', 90));

//        FullRegistrationReport();
//        Console.WriteLine(new string('═', 90));

//        StudentAverageGrades();
//        Console.WriteLine(new string('═', 90));

//        CourseAverageGrades();
//        Console.WriteLine(new string('═', 90));

//        BestStudents();

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void FillData()
//    {
//        students.Rows.Add(1, "Иванов Иван", "ivanov@university.ru");
//        students.Rows.Add(2, "Петрова Анна", "petrova@university.ru");
//        students.Rows.Add(3, "Сидоров Пётр", "sidorov@university.ru");
//        students.Rows.Add(4, "Козлова Мария", "kozlova@university.ru");
//        students.Rows.Add(5, "Михайлов Алексей", "mikhailov@university.ru");

//        courses.Rows.Add(101, "Математика", "Смирнов А.В.");
//        courses.Rows.Add(102, "Программирование", "Кузнецова Е.С.");
//        courses.Rows.Add(103, "Базы данных", "Волков И.П.");
//        courses.Rows.Add(104, "Физика", "Орлова Т.Н.");

//        registrations.Rows.Add(1, 1, 101, new DateTime(2025, 9, 1), 5);
//        registrations.Rows.Add(2, 1, 102, new DateTime(2025, 9, 1), 4);
//        registrations.Rows.Add(3, 1, 103, new DateTime(2025, 9, 1), 5);
//        registrations.Rows.Add(4, 2, 101, new DateTime(2025, 9, 2), 5);
//        registrations.Rows.Add(5, 2, 102, new DateTime(2025, 9, 2), 5);
//        registrations.Rows.Add(6, 3, 102, new DateTime(2025, 9, 3), 5);
//        registrations.Rows.Add(7, 3, 103, new DateTime(2025, 9, 3), 4);
//        registrations.Rows.Add(8, 4, 101, new DateTime(2025, 9, 4), 3);
//        registrations.Rows.Add(9, 5, 104, new DateTime(2025, 9, 5), 5);
//        registrations.Rows.Add(10, 4, 103, new DateTime(2025, 9, 6), DBNull.Value);
//    }

//    static void StudentFullCoursesAndGrades(int studentId)
//    {
//        var student = students.Rows.Find(studentId);
//        if (student == null) { Console.WriteLine($"Студент {studentId} не найден.\n"); return; }

//        Console.WriteLine($"КУРСЫ И ОЦЕНКИ студента: {student["StudentName"]}\n");
//        var regs = student.GetChildRows(relStudentReg);

//        if (regs.Length == 0) { Console.WriteLine("    Нет записей на курсы.\n"); return; }

//        foreach (var reg in regs)
//        {
//            var course = reg.GetParentRow(relCourseReg);
//            string grade = reg["Grade"] == DBNull.Value ? "не выставлена" : reg["Grade"].ToString();
//            Console.WriteLine($"    {course["CourseName"],-25} | Преподаватель: {course["Instructor"]} | Оценка: {grade}");
//        }
//        Console.WriteLine();
//    }

//    static void CourseFullStudentsAndGrades(int courseId)
//    {
//        var course = courses.Rows.Find(courseId);
//        if (course == null) { Console.WriteLine($"Курс {courseId} не найден.\n"); return; }

//        Console.WriteLine($"СТУДЕНТЫ И ОЦЕНКИ на курсе: {course["CourseName"]}\n");
//        var regs = course.GetChildRows(relCourseReg);

//        if (regs.Length == 0) { Console.WriteLine("    На курсе нет студентов.\n"); return; }

//        foreach (var reg in regs)
//        {
//            var student = reg.GetParentRow(relStudentReg);
//            string grade = reg["Grade"] == DBNull.Value ? "—" : reg["Grade"].ToString();
//            Console.WriteLine($"    {student["StudentName"],-20} | Email: {student["Email"]} | Оценка: {grade}");
//        }
//        Console.WriteLine();
//    }

//    static void FindStudentsWithCommonCourses()
//    {
//        Console.WriteLine("СТУДЕНТЫ, ИМЕЮЩИЕ ОБЩИЕ КУРСЫ:\n");
//        var studentList = students.AsEnumerable().ToList();

//        for (int i = 0; i < studentList.Count; i++)
//        {
//            for (int j = i + 1; j < studentList.Count; j++)
//            {
//                var s1 = studentList[i];
//                var s2 = studentList[j];

//                var courses1 = s1.GetChildRows(relStudentReg).Select(r => (int)r["CourseID"]).ToHashSet();
//                var courses2 = s2.GetChildRows(relStudentReg).Select(r => (int)r["CourseID"]).ToHashSet();

//                courses1.IntersectWith(courses2);

//                if (courses1.Count > 0)
//                {
//                    Console.WriteLine($"{s1["StudentName"]} и {s2["StudentName"]} учатся вместе на курсов: {courses1.Count}");
//                    foreach (int cid in courses1)
//                    {
//                        var c = courses.Rows.Find(cid);
//                        Console.WriteLine($"    • {c["CourseName"]}");
//                    }
//                    Console.WriteLine();
//                }
//            }
//        }
//        if (studentList.Count < 2) Console.WriteLine("Нет пар студентов с общими курсами.\n");
//    }

//    static void FullRegistrationReport()
//    {
//        Console.WriteLine("ПОЛНЫЙ ОТЧЁТ ПО ВСЕМ РЕГИСТРАЦИЯМ\n");
//        Console.WriteLine($"{"Студент",-20} {"Курс",-30} {"Преподаватель",-20} {"Дата",-12} {"Оценка"}");
//        Console.WriteLine(new string('-', 100));

//        foreach (DataRow reg in registrations.Rows)
//        {
//            var student = reg.GetParentRow(relStudentReg);
//            var course = reg.GetParentRow(relCourseReg);
//            string grade = reg["Grade"] == DBNull.Value ? "—" : reg["Grade"].ToString();

//            Console.WriteLine($"{student["StudentName"],-25} {course["CourseName"],-30} {course["Instructor"],-20} " +
//                              $"{((DateTime)reg["EnrollmentDate"]):dd.MM.yyyy} {grade,6}");
//        }
//        Console.WriteLine();
//    }

//    static void StudentAverageGrades()
//    {
//        Console.WriteLine("СРЕДНИЙ БАЛЛ КАЖДОГО СТУДЕНТА\n");
//        Console.WriteLine($"{"Студент",-25} {"Курсов",-8} {"Оценок выставлено",-8} {"Средний балл"}");

//        foreach (DataRow s in students.Rows)
//        {
//            var grades = s.GetChildRows(relStudentReg)
//                          .Where(r => r["Grade"] != DBNull.Value)
//                          .Select(r => (int)r["Grade"]);

//            int countWithGrade = grades.Count();
//            int totalCourses = s.GetChildRows(relStudentReg).Length;
//            double avg = countWithGrade > 0 ? grades.Average() : 0;

//            Console.WriteLine($"{s["StudentName"],-25} {totalCourses,8} {countWithGrade,16} {avg,12:F2}");
//        }
//        Console.WriteLine();
//    }

//    static void CourseAverageGrades()
//    {
//        Console.WriteLine("СРЕДНИЙ БАЛЛ ПО КАЖДОМУ КУРСУ\n");
//        Console.WriteLine($"{"Курс",-35} {"Студентов",-10} {"Оценок выставлено",-10} {"Средний балл"}");

//        foreach (DataRow c in courses.Rows)
//        {
//            var grades = c.GetChildRows(relCourseReg)
//                          .Where(r => r["Grade"] != DBNull.Value)
//                          .Select(r => (int)r["Grade"]);

//            int countWithGrade = grades.Count();
//            int totalStudents = c.GetChildRows(relCourseReg).Length;
//            double avg = countWithGrade > 0 ? grades.Average() : 0;

//            Console.WriteLine($"{c["CourseName"],-35} {totalStudents,10} {countWithGrade,17} {avg,12:F2}");
//        }
//        Console.WriteLine();
//    }

//    static void BestStudents()
//    {
//        Console.WriteLine("ЛУЧШИЕ СТУДЕНТЫ (средний балл > 4.5)\n");

//        var best = students.AsEnumerable()
//            .Select(s =>
//            {
//                var g = s.GetChildRows(relStudentReg)
//                         .Where(r => r["Grade"] != DBNull.Value)
//                         .Select(r => (int)r["Grade"]);
//                double avg = g.Any() ? g.Average() : 0;
//                return new { Row = s, Avg = avg };
//            })
//            .Where(x => x.Avg > 4.5)
//            .OrderByDescending(x => x.Avg);

//        if (!best.Any())
//        {
//            Console.WriteLine("    Нет студентов со средним баллом выше 4.5\n");
//            return;
//        }

//        foreach (var item in best)
//        {
//            Console.WriteLine($" {item.Row["StudentName"]} — средний балл: {item.Avg:F2}");
//            var regs = item.Row.GetChildRows(relStudentReg);
//            foreach (var reg in regs)
//            {
//                var course = reg.GetParentRow(relCourseReg);
//                string grade = reg["Grade"] == DBNull.Value ? "—" : reg["Grade"].ToString();
//                Console.WriteLine($"    • {course["CourseName"]} → оценка: {grade}");
//            }
//            Console.WriteLine();
//        }
//    }
//}

//8
//using System;
//using System.Data;

//class Program
//{
//    static DataSet dataSet;
//    static DataTable categories;
//    static DataTable products;
//    static DataRelation relation;

//    static void Main()
//    {
//        dataSet = new DataSet();

//        categories = new DataTable("Категории");
//        categories.Columns.Add("CategoryID", typeof(int));
//        categories.Columns.Add("CategoryName", typeof(string));
//        categories.Columns.Add("TotalProductCount", typeof(int));
//        categories.Columns.Add("TotalCategoryValue", typeof(decimal));
//        categories.PrimaryKey = new[] { categories.Columns["CategoryID"] };

//        products = new DataTable("Товары");
//        products.Columns.Add("ProductID", typeof(int));
//        products.Columns.Add("ProductName", typeof(string));
//        products.Columns.Add("Price", typeof(decimal));
//        products.Columns.Add("CategoryID", typeof(int));
//        products.PrimaryKey = new[] { products.Columns["ProductID"] };

//        dataSet.Tables.Add(categories);
//        dataSet.Tables.Add(products);

//        relation = new DataRelation("Категория_Товары",
//            categories.Columns["CategoryID"],
//            products.Columns["CategoryID"],
//            createConstraints: false);

//        dataSet.Relations.Add(relation);

//        categories.Columns["TotalProductCount"].Expression = "Count(Child.ProductID)";
//        categories.Columns["TotalCategoryValue"].Expression = "Sum(Child.Price)";

//        FillData();

//        Console.WriteLine("АГРЕГАЦИЯ ЧЕРЕЗ ВЫРАЖЕНИЯ В DATACOLUMN\n");
//        PrintCategories();

//        Console.WriteLine("\nДобавляем новый товар...");
//        products.Rows.Add(11, "Ноутбук Pro", 129990m, 2);
//        PrintCategories();

//        Console.WriteLine("\nУдаляем товар ID=4 (iPhone)...");
//        var rowToDelete = products.Rows.Find(4);
//        rowToDelete?.Delete();
//        PrintCategories();

//        Console.WriteLine("\nИзменяем цену товара...");
//        var row = products.Rows.Find(5);
//        if (row != null) row["Price"] = 27990m;
//        PrintCategories();

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void FillData()
//    {
//        categories.Rows.Add(1, "Напитки", 0, 0m);
//        categories.Rows.Add(2, "Электроника", 0, 0m);
//        categories.Rows.Add(3, "Продукты", 0, 0m);

//        products.Rows.Add(1, "Кола 1л", 85.50m, 1);
//        products.Rows.Add(2, "Сок", 120m, 1);
//        products.Rows.Add(3, "Вода", 45m, 1);
//        products.Rows.Add(4, "iPhone 15", 89990m, 2);
//        products.Rows.Add(5, "AirPods", 24990m, 2);
//        products.Rows.Add(6, "Молоко", 79m, 3);
//        products.Rows.Add(7, "Хлеб", 65m, 3);
//        products.Rows.Add(8, "Сыр", 450m, 3);
//    }

//    static void PrintCategories()
//    {
//        Console.WriteLine($"{"Категория",-20} {"Товаров",8} {"Общая стоимость",18}");
//        Console.WriteLine(new string('─', 50));

//        foreach (DataRow cat in categories.Rows)
//        {
//            string name = cat["CategoryName"].ToString();
//            int count = Convert.ToInt32(cat["TotalProductCount"]);
//            decimal total = Convert.ToDecimal(cat["TotalCategoryValue"]);

//            Console.WriteLine($"{name,-20} {count,8} {total,18:C}");
//        }
//        Console.WriteLine();
//    }
//}

//9
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("ДЕМОНСТРАЦИЯ DeleteRule В ОТНОШЕНИЯХ DataSet\n");

//        DemonstrateRule(DeleteRule.Cascade, "CASCADE");
//        DemonstrateRule(DeleteRule.SetNull, "SETNULL");
//        DemonstrateRule(DeleteRule.None, "NONE");

//        Console.WriteLine("Готово.");
//        Console.ReadKey();
//    }

//    static void DemonstrateRule(DeleteRule rule, string ruleName)
//    {
//        Console.WriteLine(new string('=', 70));
//        Console.WriteLine($"ПРАВИЛО: {ruleName}");
//        Console.WriteLine(new string('=', 70));

//        var ds = new DataSet();

//        var departments = new DataTable("Отделы");
//        departments.Columns.Add("DepartmentID", typeof(int));
//        departments.Columns.Add("DepartmentName", typeof(string));
//        departments.PrimaryKey = new[] { departments.Columns["DepartmentID"] };

//        var employees = new DataTable("Сотрудники");
//        employees.Columns.Add("EmployeeID", typeof(int));
//        employees.Columns.Add("EmployeeName", typeof(string));
//        employees.Columns.Add("DepartmentID", typeof(int)).AllowDBNull = true;
//        employees.Columns.Add("Salary", typeof(decimal));

//        ds.Tables.Add(departments);
//        ds.Tables.Add(employees);

//        departments.Rows.Add(1, "IT-отдел");
//        departments.Rows.Add(2, "Бухгалтерия");
//        departments.Rows.Add(3, "HR");

//        employees.Rows.Add(101, "Иванов", 1, 200000m);
//        employees.Rows.Add(102, "Петров", 1, 180000m);
//        employees.Rows.Add(103, "Сидорова", 2, 150000m);
//        employees.Rows.Add(104, "Козлов", null, 170000m);

//        var relation = new DataRelation("Отдел_Сотрудники",
//            departments.Columns["DepartmentID"],
//            employees.Columns["DepartmentID"],
//            createConstraints: true);

//        relation.ChildKeyConstraint.DeleteRule = rule;
//        if (rule == DeleteRule.SetNull)
//            employees.Columns["DepartmentID"].AllowDBNull = true;

//        ds.Relations.Add(relation);

//        PrintState(ds, "ИСХОДНОЕ СОСТОЯНИЕ");

//        try
//        {
//            var deptToDelete = departments.Rows.Find(1);
//            Console.WriteLine($"Пытаемся удалить отдел \"{deptToDelete["DepartmentName"]}\" (ID=1)...\n");
//            deptToDelete.Delete();
//            ds.AcceptChanges();

//            Console.WriteLine("Удаление прошло УСПЕШНО.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"ОШИБКА: {ex.Message}");
//        }

//        PrintState(ds, $"СОСТОЯНИЕ ПОСЛЕ ПОПЫТКИ УДАЛЕНИЯ ({ruleName})");
//        Console.WriteLine();
//    }

//    static void PrintState(DataSet ds, string title)
//    {
//        Console.WriteLine($"\n{title}");
//        Console.WriteLine(new string('-', 60));

//        Console.WriteLine("Отделы:");
//        foreach (DataRow r in ds.Tables["Отделы"].Rows)
//        {
//            if (r.RowState == DataRowState.Deleted)
//                Console.WriteLine($"  [УДАЛЁН] {r["DepartmentName", DataRowVersion.Original]}");
//            else
//                Console.WriteLine($"  {r["DepartmentID"]} — {r["DepartmentName"]}");
//        }

//        Console.WriteLine("\nСотрудники:");
//        Console.WriteLine($"{"ID",-4} {"Имя",-15} {"Отдел ID",-10} {"Зарплата",10}");
//        foreach (DataRow r in ds.Tables["Сотрудники"].Rows)
//        {
//            if (r.RowState == DataRowState.Deleted)
//                Console.WriteLine($"  [УДАЛЁН] {r["EmployeeName", DataRowVersion.Original]}");
//            else
//            {
//                string dept = r["DepartmentID"] == DBNull.Value ? "NULL" : r["DepartmentID"].ToString();
//                Console.WriteLine($"  {r["EmployeeID"],-4} {r["EmployeeName"],-15} {dept,-10} {r["Salary"],10:C}");
//            }
//        }
//        Console.WriteLine();
//    }
//}

//10
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        Console.WriteLine("ДЕМОНСТРАЦИЯ UpdateRule В ОТНОШЕНИЯХ DataSet\n");

//        DemonstrateUpdateRule(UpdateRule.Cascade, "CASCADE");
//        DemonstrateUpdateRule(UpdateRule.SetNull, "SETNULL");
//        DemonstrateUpdateRule(UpdateRule.None, "NONE");

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void DemonstrateUpdateRule(UpdateRule rule, string ruleName)
//    {
//        Console.WriteLine(new string('=', 80));
//        Console.WriteLine($"ПРАВИЛО ОБНОВЛЕНИЯ: {ruleName}");
//        Console.WriteLine(new string('=', 80));

//        var ds = new DataSet();

//        var departments = new DataTable("Отделы");
//        departments.Columns.Add("DepartmentID", typeof(int));
//        departments.Columns.Add("DepartmentName", typeof(string));
//        departments.PrimaryKey = new[] { departments.Columns["DepartmentID"] };

//        var employees = new DataTable("Сотрудники");
//        employees.Columns.Add("EmployeeID", typeof(int));
//        employees.Columns.Add("EmployeeName", typeof(string));
//        employees.Columns.Add("DepartmentID", typeof(int)).AllowDBNull = true;
//        employees.Columns.Add("Salary", typeof(decimal));

//        ds.Tables.Add(departments);
//        ds.Tables.Add(employees);

//        departments.Rows.Add(10, "IT-отдел");
//        departments.Rows.Add(20, "Бухгалтерия");
//        departments.Rows.Add(30, "HR");

//        employees.Rows.Add(101, "Иванов Иван", 10, 250000m);
//        employees.Rows.Add(102, "Петров Пётр", 10, 220000m);
//        employees.Rows.Add(103, "Сидорова Анна", 20, 180000m);
//        employees.Rows.Add(104, "Козлов Алексей", null, 200000m);

//        var relation = new DataRelation("Отдел_Сотрудники",
//            departments.Columns["DepartmentID"],
//            employees.Columns["DepartmentID"],
//            createConstraints: true);

//        relation.ChildKeyConstraint.UpdateRule = rule;

//        ds.Relations.Add(relation);

//        PrintState(ds, "ИСХОДНОЕ СОСТОЯНИЕ");

//        try
//        {
//            var dept = departments.Rows.Find(10);
//            Console.WriteLine($"Меняем DepartmentID с 10 → 100 в отделе \"{dept["DepartmentName"]}\"...\n");
//            dept["DepartmentID"] = 100;

//            ds.AcceptChanges();

//            Console.WriteLine("Обновление прошло УСПЕШНО.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"ОШИБКА: {ex.Message}");
//            ds.RejectChanges();
//        }

//        PrintState(ds, $"СОСТОЯНИЕ ПОСЛЕ ПОПЫТКИ ИЗМЕНЕНИЯ ({ruleName})");
//        Console.WriteLine();
//    }

//    static void PrintState(DataSet ds, string title)
//    {
//        Console.WriteLine($"\n{title}");
//        Console.WriteLine(new string('-', 70));

//        Console.WriteLine("Отделы:");
//        Console.WriteLine($"{"ID",-6} {"Название"}");
//        foreach (DataRow r in ds.Tables["Отделы"].Rows)
//        {
//            if (r.RowState == DataRowState.Modified)
//            {
//                Console.WriteLine($"  {r["DepartmentID", DataRowVersion.Original]} → {r["DepartmentID"]}  {r["DepartmentName"]} (изменён)");
//            }
//            else
//            {
//                Console.WriteLine($"  {r["DepartmentID"],-6} {r["DepartmentName"]}");
//            }
//        }

//        Console.WriteLine("\nСотрудники:");
//        Console.WriteLine($"{"ID",-4} {"Имя",-18} {"Отдел ID",-10} {"Зарплата",12}");
//        foreach (DataRow r in ds.Tables["Сотрудники"].Rows)
//        {
//            if (r.RowState == DataRowState.Modified)
//            {
//                string oldDept = r["DepartmentID", DataRowVersion.Original] == DBNull.Value ? "NULL" : r["DepartmentID", DataRowVersion.Original].ToString();
//                string newDept = r["DepartmentID"] == DBNull.Value ? "NULL" : r["DepartmentID"].ToString();
//                Console.WriteLine($"  {r["EmployeeID"],-4} {r["EmployeeName"],-18} {oldDept} → {newDept} {r["Salary"],12:C} (изменён)");
//            }
//            else
//            {
//                string dept = r["DepartmentID"] == DBNull.Value ? "NULL" : r["DepartmentID"].ToString();
//                Console.WriteLine($"  {r["EmployeeID"],-4} {r["EmployeeName"],-18} {dept,-10} {r["Salary"],12:C}");
//            }
//        }
//        Console.WriteLine();
//    }
//}

//11
//using System;
//using System.Data;

//class Program
//{
//    static DataSet dataSet;
//    static DataTable customers, orders, orderDetails;
//    static DataRelation relCustOrd, relOrdDet;

//    static void Main()
//    {
//        dataSet = new DataSet("УправлениеЗаказами");

//        customers = new DataTable("Заказчики");
//        customers.Columns.Add("CustomerID", typeof(int));
//        customers.Columns.Add("CustomerName", typeof(string));
//        customers.Columns.Add("Email", typeof(string));
//        customers.PrimaryKey = new[] { customers.Columns["CustomerID"] };

//        orders = new DataTable("Заказы");
//        orders.Columns.Add("OrderID", typeof(int));
//        orders.Columns.Add("OrderDate", typeof(DateTime));
//        orders.Columns.Add("CustomerID", typeof(int));
//        orders.Columns.Add("Total", typeof(decimal));
//        orders.PrimaryKey = new[] { orders.Columns["OrderID"] };

//        orderDetails = new DataTable("ДеталиЗаказа");
//        orderDetails.Columns.Add("DetailID", typeof(int));
//        orderDetails.Columns.Add("OrderID", typeof(int));
//        orderDetails.Columns.Add("ProductID", typeof(int));
//        orderDetails.Columns.Add("Quantity", typeof(int));
//        orderDetails.Columns.Add("Price", typeof(decimal));
//        orderDetails.PrimaryKey = new[] { orderDetails.Columns["DetailID"] };

//        dataSet.Tables.Add(customers);
//        dataSet.Tables.Add(orders);
//        dataSet.Tables.Add(orderDetails);

//        relCustOrd = new DataRelation("Заказчик_Заказы",
//            customers.Columns["CustomerID"], orders.Columns["CustomerID"], true);
//        relCustOrd.ChildKeyConstraint.DeleteRule = Rule.Cascade;
//        relCustOrd.ChildKeyConstraint.UpdateRule = Rule.Cascade;

//        relOrdDet = new DataRelation("Заказ_Детали",
//            orders.Columns["OrderID"], orderDetails.Columns["OrderID"], true);
//        relOrdDet.ChildKeyConstraint.DeleteRule = Rule.Cascade;
//        relOrdDet.ChildKeyConstraint.UpdateRule = Rule.Cascade;

//        dataSet.Relations.Add(relCustOrd);
//        dataSet.Relations.Add(relOrdDet);

//        FillTestData();

//        Console.WriteLine("СИСТЕМА УПРАВЛЕНИЯ ЗАКАЗАМИ С CASCADE ПРАВИЛАМИ\n");

//        PrintFullReport("ИСХОДНОЕ СОСТОЯНИЕ");

//        AddCustomer(4, "Смирнов Сергей", "smirnov@email.com");
//        PrintFullReport("ПОСЛЕ ДОБАВЛЕНИЯ ЗАКАЗЧИКА");

//        AddOrder(104, new DateTime(2025, 12, 1), 1, 15000m);
//        PrintFullReport("ПОСЛЕ ДОБАВЛЕНИЯ ЗАКАЗА");

//        ChangeCustomerId(1, 100);
//        PrintFullReport("ПОСЛЕ ИЗМЕНЕНИЯ ID ЗАКАЗЧИКА (1 → 100)");

//        ChangeOrderId(101, 1001);
//        PrintFullReport("ПОСЛЕ ИЗМЕНЕНИЯ ID ЗАКАЗА (101 → 1001)");

//        DeleteOrder(102);
//        PrintFullReport("ПОСЛЕ УДАЛЕНИЯ ЗАКАЗА ID=102");

//        DeleteCustomer(2);
//        PrintFullReport("ПОСЛЕ УДАЛЕНИЯ ЗАКАЗЧИКА ID=2");

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void FillTestData()
//    {
//        customers.Rows.Add(1, "Иванов Иван", "ivanov@email.com");
//        customers.Rows.Add(2, "Петрова Анна", "petrova@email.com");
//        customers.Rows.Add(3, "Сидоров Пётр", "sidorov@email.com");

//        orders.Rows.Add(101, new DateTime(2025, 11, 1), 1, 5000m);
//        orders.Rows.Add(102, new DateTime(2025, 11, 5), 1, 8000m);
//        orders.Rows.Add(103, new DateTime(2025, 11, 10), 2, 3000m);

//        orderDetails.Rows.Add(1, 101, 10, 2, 2500m);
//        orderDetails.Rows.Add(2, 101, 11, 1, 2500m);
//        orderDetails.Rows.Add(3, 102, 12, 4, 2000m);
//        orderDetails.Rows.Add(4, 103, 13, 3, 1000m);
//    }

//    static void AddCustomer(int id, string name, string email)
//    {
//        Console.WriteLine($"\nДОБАВЛЕНИЕ заказчика ID={id}...");
//        try
//        {
//            customers.Rows.Add(id, name, email);
//            Console.WriteLine("Заказчик добавлен успешно.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//    }

//    static void AddOrder(int orderId, DateTime date, int customerId, decimal total)
//    {
//        Console.WriteLine($"\nДОБАВЛЕНИЕ заказа ID={orderId} для заказчика {customerId}...");
//        try
//        {
//            orders.Rows.Add(orderId, date, customerId, total);
//            Console.WriteLine("Заказ добавлен успешно.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//    }

//    static void DeleteCustomer(int customerId)
//    {
//        Console.WriteLine($"\nУДАЛЕНИЕ заказчика ID={customerId} (CASCADE: удалятся заказы и детали)...");
//        var cust = customers.Rows.Find(customerId);
//        if (cust == null)
//        {
//            Console.WriteLine("Заказчик не найден.");
//            return;
//        }

//        var childOrders = cust.GetChildRows(relCustOrd);
//        int ordersCount = childOrders.Length;
//        int detailsCount = 0;
//        foreach (DataRow ord in childOrders)
//            detailsCount += ord.GetChildRows(relOrdDet).Length;

//        try
//        {
//            cust.Delete();
//            dataSet.AcceptChanges();
//            Console.WriteLine($"Успешно удалено: 1 заказчик, {ordersCount} заказ(ов), {detailsCount} детал(ей).");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//            dataSet.RejectChanges();
//        }
//    }

//    static void DeleteOrder(int orderId)
//    {
//        Console.WriteLine($"\nУДАЛЕНИЕ заказа ID={orderId} (CASCADE: удалятся детали)...");
//        var ord = orders.Rows.Find(orderId);
//        if (ord == null)
//        {
//            Console.WriteLine("Заказ не найден.");
//            return;
//        }

//        int detailsCount = ord.GetChildRows(relOrdDet).Length;

//        try
//        {
//            ord.Delete();
//            dataSet.AcceptChanges();
//            Console.WriteLine($"Успешно удалено: 1 заказ, {detailsCount} детал(ей).");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//            dataSet.RejectChanges();
//        }
//    }

//    static void ChangeCustomerId(int oldId, int newId)
//    {
//        Console.WriteLine($"\nИЗМЕНЕНИЕ CustomerID: {oldId} → {newId} (CASCADE: обновятся заказы)...");
//        var cust = customers.Rows.Find(oldId);
//        if (cust == null)
//        {
//            Console.WriteLine("Заказчик не найден.");
//            return;
//        }

//        int ordersCount = cust.GetChildRows(relCustOrd).Length;

//        try
//        {
//            cust["CustomerID"] = newId;
//            dataSet.AcceptChanges();
//            Console.WriteLine($"Успешно обновлено: 1 заказчик, {ordersCount} заказ(ов).");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//            dataSet.RejectChanges();
//        }
//    }

//    static void ChangeOrderId(int oldId, int newId)
//    {
//        Console.WriteLine($"\nИЗМЕНЕНИЕ OrderID: {oldId} → {newId} (CASCADE: обновятся детали)...");
//        var ord = orders.Rows.Find(oldId);
//        if (ord == null)
//        {
//            Console.WriteLine("Заказ не найден.");
//            return;
//        }

//        int detailsCount = ord.GetChildRows(relOrdDet).Length;

//        try
//        {
//            ord["OrderID"] = newId;
//            dataSet.AcceptChanges();
//            Console.WriteLine($"Успешно обновлено: 1 заказ, {detailsCount} детал(ей).");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//            dataSet.RejectChanges();
//        }
//    }

//    static void PrintFullReport(string title)
//    {
//        Console.WriteLine(new string('═', 80));
//        Console.WriteLine(title);
//        Console.WriteLine(new string('═', 80));

//        Console.WriteLine("ЗАКАЗЧИКИ:");
//        foreach (DataRow c in customers.Rows)
//            Console.WriteLine($"  [{c["CustomerID"]}] {c["CustomerName"]} <{c["Email"]}>");

//        Console.WriteLine("\nЗАКАЗЫ:");
//        foreach (DataRow o in orders.Rows)
//        {
//            var cust = o.GetParentRow(relCustOrd);
//            string custName = cust != null ? cust["CustomerName"].ToString() : "—";
//            Console.WriteLine($"  [{o["OrderID"]}] {o["OrderDate"]:yyyy-MM-dd} | Заказчик: {custName} | Сумма: {o["Total"]:C}");
//            var details = o.GetChildRows(relOrdDet);
//            foreach (DataRow d in details)
//                Console.WriteLine($"     → Деталь {d["DetailID"]}: Продукт {d["ProductID"]}, Кол-во: {d["Quantity"]}, Цена: {d["Price"]:C}");
//        }
//        Console.WriteLine();
//    }
//}

//12
//using System;
//using System.Data;
//using System.Linq;

//class Program
//{
//    static DataSet dataSet;
//    static DataTable categories, products;
//    static DataRelation relation;

//    static void Main()
//    {
//        dataSet = new DataSet();

//        categories = new DataTable("Категории");
//        categories.Columns.Add("CategoryID", typeof(int));
//        categories.Columns.Add("CategoryName", typeof(string));
//        categories.PrimaryKey = new[] { categories.Columns["CategoryID"] };

//        products = new DataTable("Товары");
//        products.Columns.Add("ProductID", typeof(int));
//        products.Columns.Add("ProductName", typeof(string));
//        products.Columns.Add("Price", typeof(decimal));
//        products.Columns.Add("CategoryID", typeof(int));
//        products.PrimaryKey = new[] { products.Columns["ProductID"] };

//        dataSet.Tables.Add(categories);
//        dataSet.Tables.Add(products);

//        relation = new DataRelation("Категория_Товары",
//            categories.Columns["CategoryID"],
//            products.Columns["CategoryID"],
//            createConstraints: false);

//        dataSet.Relations.Add(relation);

//        FillInitialData();

//        Console.WriteLine("ДЕМОНСТРАЦИЯ RowState И ОТСЛЕЖИВАНИЕ УДАЛЕНИЙ\n");
//        PrintCurrentState("ИСХОДНОЕ СОСТОЯНИЕ");

//        // Операции
//        AddProduct(11, "Чай зелёный", 150m, 1);
//        AddProduct(12, "Монитор 27\"", 35990m, 2);

//        ModifyProduct(1, newName: "Кола Zero 1л", newPrice: 95m);
//        ModifyProduct(5, newPrice: 28990m);

//        DeleteProduct(3);  // Вода
//        DeleteProduct(4);  // iPhone 15
//        DeleteProduct(8);  // Сыр

//        PrintCurrentState("СОСТОЯНИЕ ПОСЛЕ ОПЕРАЦИЙ (НЕ СОХРАНЕНО)");

//        PrintDeletionReport();

//        Console.WriteLine("\nОТМЕНА УДАЛЕНИЯ товара ID=4 (iPhone)...");
//        UndeleteProduct(4);

//        PrintDeletionReport();

//        Console.WriteLine("\nФИНАЛЬНОЕ СОСТОЯНИЕ ПОСЛЕ ОТМЕНЫ:");
//        PrintCurrentState("ФИНАЛЬНОЕ СОСТОЯНИЕ");

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void FillInitialData()
//    {
//        categories.Rows.Add(1, "Напитки");
//        categories.Rows.Add(2, "Электроника");
//        categories.Rows.Add(3, "Продукты");

//        products.Rows.Add(1, "Кола 1л", 85.50m, 1);
//        products.Rows.Add(2, "Сок", 120m, 1);
//        products.Rows.Add(3, "Вода", 45m, 1);
//        products.Rows.Add(4, "iPhone 15", 89990m, 2);
//        products.Rows.Add(5, "AirPods", 24990m, 2);
//        products.Rows.Add(6, "Молоко", 79m, 3);
//        products.Rows.Add(7, "Хлеб", 65m, 3);
//        products.Rows.Add(8, "Сыр", 450m, 3);
//    }

//    static void AddProduct(int id, string name, decimal price, int catId)
//    {
//        products.Rows.Add(id, name, price, catId);
//    }

//    static void ModifyProduct(int id, string? newName = null, decimal? newPrice = null)
//    {
//        var row = products.Rows.Find(id);
//        if (row != null)
//        {
//            if (newName != null) row["ProductName"] = newName;
//            if (newPrice != null) row["Price"] = newPrice.Value;
//        }
//    }

//    static void DeleteProduct(int id)
//    {
//        var row = products.Rows.Find(id);
//        if (row != null)
//            row.Delete();
//    }

//    static void UndeleteProduct(int id)
//    {
//        var row = products.Rows.Find(id);
//        if (row != null && row.RowState == DataRowState.Deleted)
//            row.RejectChanges();
//    }

//    static void PrintCurrentState(string title)
//    {
//        Console.WriteLine(new string('=', 70));
//        Console.WriteLine(title);
//        Console.WriteLine(new string('=', 70));

//        foreach (DataRow cat in categories.Rows)
//        {
//            Console.WriteLine($"Категория: {cat["CategoryName"]}");
//            var prods = cat.GetChildRows(relation);
//            foreach (DataRow p in prods)
//            {
//                string state = p.RowState switch
//                {
//                    DataRowState.Added => " [ДОБАВЛЕН]",
//                    DataRowState.Modified => " [ИЗМЕНЁН]",
//                    DataRowState.Deleted => " [УДАЛЁН]",
//                    _ => ""
//                };
//                string name = p.RowState == DataRowState.Deleted
//                    ? p["ProductName", DataRowVersion.Original].ToString()
//                    : p["ProductName"].ToString();

//                Console.WriteLine($"   • {name} | Цена: {p["Price", p.RowState == DataRowState.Deleted ? DataRowVersion.Original : DataRowVersion.Current]:C} {state}");
//            }
//        }
//        Console.WriteLine();
//    }

//    static void PrintDeletionReport()
//    {
//        Console.WriteLine("\nОТЧЁТ: ТОВАРЫ, ПОМЕЧЕННЫЕ НА УДАЛЕНИЕ\n");
//        Console.WriteLine($"{"ID",-4} {"Товар",-25} {"Категория",-15} {"Цена (было)",-12} {"Останется в категории"}");
//        Console.WriteLine(new string('-', 80));

//        var deletedRows = products.Select(null, null, DataViewRowState.Deleted);

//        if (deletedRows.Length == 0)
//        {
//            Console.WriteLine("    Нет товаров, помеченных на удаление.\n");
//            return;
//        }

//        foreach (DataRow row in deletedRows)
//        {
//            int id = (int)row["ProductID", DataRowVersion.Original];
//            string name = row["ProductName", DataRowVersion.Original].ToString();
//            decimal price = (decimal)row["Price", DataRowVersion.Original];
//            int catId = (int)row["CategoryID", DataRowVersion.Original];

//            var categoryRow = categories.Rows.Find(catId);
//            string catName = categoryRow != null ? categoryRow["CategoryName"].ToString() : "Неизвестно";

//            int currentCount = categoryRow?.GetChildRows(relation).Length ?? 0;
//            int remainingAfterDelete = currentCount - 1;

//            Console.WriteLine($"{id,-4} {name,-25} {catName,-15} {price,-12:C} {remainingAfterDelete}");
//        }

//        Console.WriteLine($"\nВсего помечено на удаление: {deletedRows.Length} товар(ов)\n");
//    }
//}

//13
//using System;
//using System.Data;
//using System.Linq;

//class Program
//{
//    static DataSet dataSet;
//    static DataTable students, courses, registrations;
//    static DataRelation relStudentReg, relCourseReg;

//    static void Main()
//    {
//        dataSet = new DataSet();

//        students = new DataTable("Студенты");
//        students.Columns.Add("StudentID", typeof(int));
//        students.Columns.Add("StudentName", typeof(string));
//        students.PrimaryKey = new[] { students.Columns["StudentID"] };

//        courses = new DataTable("Курсы");
//        courses.Columns.Add("CourseID", typeof(int));
//        courses.Columns.Add("CourseName", typeof(string));
//        courses.Columns.Add("Instructor", typeof(string));
//        courses.PrimaryKey = new[] { courses.Columns["CourseID"] };

//        registrations = new DataTable("Регистрация");
//        registrations.Columns.Add("RegistrationID", typeof(int));
//        registrations.Columns.Add("StudentID", typeof(int));
//        registrations.Columns.Add("CourseID", typeof(int));
//        registrations.Columns.Add("EnrollmentDate", typeof(DateTime));
//        registrations.PrimaryKey = new[] { registrations.Columns["RegistrationID"] };

//        dataSet.Tables.Add(students);
//        dataSet.Tables.Add(courses);
//        dataSet.Tables.Add(registrations);

//        relStudentReg = new DataRelation("Студент_Регистрации",
//            students.Columns["StudentID"], registrations.Columns["StudentID"], false);
//        relCourseReg = new DataRelation("Курс_Регистрации",
//            courses.Columns["CourseID"], registrations.Columns["CourseID"], false);

//        dataSet.Relations.Add(relStudentReg);
//        dataSet.Relations.Add(relCourseReg);

//        FillBaseData();

//        Console.WriteLine("АНАЛИЗ НОВЫХ РЕГИСТРАЦИЙ (RowState.Added)\n");

//        // Добавляем новые регистрации (ещё НЕ сохранены)
//        AddNewRegistration(10, 1, 101, new DateTime(2025, 9, 1));
//        AddNewRegistration(11, 2, 102, new DateTime(2025, 9, 2));
//        AddNewRegistration(12, 3, 101, new DateTime(2025, 9, 3));
//        AddNewRegistration(13, 1, 103, new DateTime(2025, 9, 4));
//        AddNewRegistration(14, 5, 999, new DateTime(2025, 9, 5)); // НЕСУЩЕСТВУЮЩИЙ КУРС!
//        AddNewRegistration(15, 999, 101, new DateTime(2025, 9, 6)); // НЕСУЩЕСТВУЮЩИЙ СТУДЕНТ!

//        PrintNewRegistrationsReport();

//        PrintNewRegistrationsStatistics();

//        Console.WriteLine("\nГотово. Изменения не сохранены только в памяти.");
//        Console.ReadKey();
//    }

//    static void FillBaseData()
//    {
//        students.Rows.Add(1, "Иванов Иван");
//        students.Rows.Add(2, "Петрова Анна");
//        students.Rows.Add(3, "Сидоров Пётр");
//        students.Rows.Add(4, "Козлова Мария");

//        courses.Rows.Add(101, "Математика", "Смирнов А.В.");
//        courses.Rows.Add(102, "Программирование", "Кузнецова Е.С.");
//        courses.Rows.Add(103, "Базы данных", "Волков И.П.");

//        // Уже существующие регистрации (сохранённые)
//        registrations.Rows.Add(1, 1, 101, new DateTime(2025, 1, 10));
//        registrations.Rows.Add(2, 2, 101, new DateTime(2025, 1, 11));
//    }

//    static void AddNewRegistration(int regId, int studentId, int courseId, DateTime date)
//    {
//        var row = registrations.NewRow();
//        row["RegistrationID"] = regId;
//        row["StudentID"] = studentId;
//        row["CourseID"] = courseId;
//        row["EnrollmentDate"] = date;
//        registrations.Rows.Add(row);
//        // AcceptChanges() НЕ вызываем — строка остаётся в состоянии Added
//    }

//    static void PrintNewRegistrationsReport()
//    {
//        var newRegs = registrations.Select(null, null, DataViewRowState.Added);

//        Console.WriteLine("ОТЧЁТ: НОВЫЕ РЕГИСТРАЦИИ (RowState = Added)\n");
//        Console.WriteLine($"{"ID",-4} {"Студент",-20} {"Курс",-25} {"Дата",-12} {"Статус"}");
//        Console.WriteLine(new string('─', 85));

//        foreach (DataRow reg in newRegs)
//        {
//            int regId = (int)reg["RegistrationID"];
//            int studentId = (int)reg["StudentID"];
//            int courseId = (int)reg["CourseID"];
//            DateTime date = (DateTime)reg["EnrollmentDate"];

//            var studentRow = reg.GetParentRow(relStudentReg);
//            var courseRow = reg.GetParentRow(relCourseReg);

//            string studentName = studentRow != null ? studentRow["StudentName"].ToString() : $"[НЕИЗВЕСТЕН ID={studentId}]";
//            string courseName = courseRow != null ? courseRow["CourseName"].ToString() : $"[НЕИЗВЕСТЕН ID={courseId}]";
//            string status = (studentRow == null || courseRow == null) ? "ОШИБКА!" : "ОК";

//            Console.ForegroundColor = status == "ОШИБКА!" ? ConsoleColor.Red : ConsoleColor.Gray;
//            Console.WriteLine($"{regId,-4} {studentName,-20} {courseName,-25} {date:dd.MM.yyyy} {status}");
//            Console.ResetColor();
//        }

//        Console.WriteLine($"\nВсего новых регистраций: {newRegs.Length}\n");
//    }

//    static void PrintNewRegistrationsStatistics()
//    {
//        var newRegs = registrations.Select(null, null, DataViewRowState.Added);

//        Console.WriteLine("СТАТИСТИКА НОВЫХ РЕГИСТРАЦИЙ\n");

//        var byStudent = newRegs
//            .GroupBy(r => r["StudentID"])
//            .Select(g => new
//            {
//                StudentID = (int)g.Key,
//                Student = registrations.Rows.Cast<DataRow>()
//                    .FirstOrDefault(r => r.RowState != DataRowState.Deleted && (int)r["StudentID"] == (int)g.Key)
//                    ?.GetParentRow(relStudentReg),
//                Count = g.Count()
//            });

//        Console.WriteLine("По студентам:");
//        foreach (var item in byStudent)
//        {
//            string name = item.Student != null ? item.Student["StudentName"].ToString() : $"[Неизвестен ID={item.StudentID}]";
//            Console.WriteLine($"  • {name} → {item.Count} нов. регистраций");
//        }

//        var byCourse = newRegs
//            .GroupBy(r => r["CourseID"])
//            .Select(g => new
//            {
//                CourseID = (int)g.Key,
//                Course = registrations.Rows.Cast<DataRow>()
//                    .FirstOrDefault(r => r.RowState != DataRowState.Deleted && (int)r["CourseID"] == (int)g.Key)
//                    ?.GetParentRow(relCourseReg),
//                Count = g.Count()
//            });

//        Console.WriteLine("\nПо курсам:");
//        foreach (var item in byCourse)
//        {
//            string name = item.Course != null ? item.Course["CourseName"].ToString() : $"[Неизвестен ID={item.CourseID}]";
//            Console.WriteLine($"  • {name} → {item.Count} нов. регистраций");
//        }
//    }

//    static void ConsoleLine(string text)
//    {
//        Console.WriteLine(new string('═', 90));
//        Console.WriteLine(text);
//        Console.WriteLine(new string('═', 90));
//    }
//}

//14
//using System;
//using System.Data;
//using System.Linq;

//class Program
//{
//    static DataSet ds;
//    static DataTable students, courses, registrations;
//    static DataRelation relStudent, relCourse;

//    static void Main()
//    {
//        ds = new DataSet();

//        students = new DataTable("Студенты");
//        students.Columns.Add("StudentID", typeof(int));
//        students.Columns.Add("StudentName", typeof(string));
//        students.PrimaryKey = new[] { students.Columns["StudentID"] };

//        courses = new DataTable("Курсы");
//        courses.Columns.Add("CourseID", typeof(int));
//        courses.Columns.Add("CourseName", typeof(string));
//        courses.PrimaryKey = new[] { courses.Columns["CourseID"] };

//        registrations = new DataTable("Регистрация");
//        registrations.Columns.Add("RegID", typeof(int));
//        registrations.Columns.Add("StudentID", typeof(int));
//        registrations.Columns.Add("CourseID", typeof(int));
//        registrations.Columns.Add("Grade", typeof(int)).AllowDBNull = true;
//        registrations.PrimaryKey = new[] { registrations.Columns["RegID"] };

//        ds.Tables.AddRange(new[] { students, courses, registrations });

//        relStudent = new DataRelation("Student_Regs",
//            students.Columns["StudentID"], registrations.Columns["StudentID"], false);
//        relCourse = new DataRelation("Course_Regs",
//            courses.Columns["CourseID"], registrations.Columns["CourseID"], false);

//        ds.Relations.AddRange(new[] { relStudent, relCourse });

//        FillData();
//        ds.AcceptChanges(); // фиксируем текущее состояние как "оригинал"

//        Console.WriteLine("АНАЛИЗ ИЗМЕНЁННЫХ ЗАПИСЕЙ (RowState.Modified)\n");

//        // Изменяем оценки
//        SetGrade(1, 4);   // было 5 → 4
//        SetGrade(2, 5);   // было 3 → 5
//        SetGrade(3, 5);   // было 5 → 5 (без изменений)
//        SetGrade(4, 2);   // было 4 → 2
//        SetGrade(5, 6);   // недопустимо → отклонено

//        PrintModifiedReport();
//        PrintChangeStatistics();

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void FillData()
//    {
//        students.Rows.Add(1, "Иванов Иван");
//        students.Rows.Add(2, "Петрова Анна");
//        students.Rows.Add(3, "Сидоров Пётр");

//        courses.Rows.Add(101, "Математика");
//        courses.Rows.Add(102, "Программирование");
//        courses.Rows.Add(103, "Базы данных");

//        registrations.Rows.Add(1, 1, 101, 5);
//        registrations.Rows.Add(2, 1, 102, 3);
//        registrations.Rows.Add(3, 2, 101, 5);
//        registrations.Rows.Add(4, 2, 103, 4);
//        registrations.Rows.Add(5, 3, 102, 4);
//    }

//    static bool SetGrade(int regId, int newGrade)
//    {
//        var row = registrations.Rows.Find(regId);
//        if (row == null) return false;

//        if (newGrade < 2 || newGrade > 5)
//        {
//            Console.WriteLine($"ОШИБКА: Оценка {newGrade} недопустима (должна быть 2–5) — регистрация {regId} не изменена.");
//            return false;
//        }

//        row["Grade"] = newGrade;
//        return true;
//    }

//    static void PrintModifiedReport()
//    {
//        var modified = registrations.Select(null, null, DataViewRowState.Modified);

//        Console.WriteLine("ИЗМЕНЁННЫЕ РЕГИСТРАЦИИ\n");
//        Console.WriteLine($"{"ID",-4} {"Студент",-18} {"Курс",-20} {"Старая",-6} {"Новая",-6} {"Дельта",-8} {"Статус"}");
//        Console.WriteLine(new string('─', 80));

//        foreach (DataRow r in modified)
//        {
//            int oldG = (int)r["Grade", DataRowVersion.Original];
//            int newG = (int)r["Grade", DataRowVersion.Current];
//            int delta = newG - oldG;

//            var student = r.GetParentRow(relStudent);
//            var course = r.GetParentRow(relCourse);

//            string sName = student?["StudentName"]?.ToString() ?? "[нет]";
//            string cName = course?["CourseName"]?.ToString() ?? "[нет]";

//            string status = delta > 0 ? "ПОВЫШЕНО" :
//                           delta < 0 ? "ПОНИЖЕНО" : "БЕЗ ИЗМЕНЕНИЙ";

//            Console.WriteLine($"{r["RegID"],-4} {sName,-18} {cName,-20} {oldG,-6} {newG,-6} {delta,+8} {status}");
//        }

//        if (modified.Length == 0)
//            Console.WriteLine("    Нет изменённых записей.\n");
//        else
//            Console.WriteLine($"\nВсего изменено: {modified.Length} регистраций\n");
//    }

//    static void PrintChangeStatistics()
//    {
//        var modified = registrations.Select(null, null, DataViewRowState.Modified);

//        int up = 0, down = 0, same = 0;

//        foreach (DataRow r in modified)
//        {
//            int oldG = (int)r["Grade", DataRowVersion.Original];
//            int newG = (int)r["Grade", DataRowVersion.Current];
//            if (newG > oldG) up++;
//            else if (newG < oldG) down++;
//            else same++;
//        }

//        Console.WriteLine("СТАТИСТИКА ИЗМЕНЕНИЙ ОЦЕНОК");
//        Console.WriteLine(new string('─', 40));
//        Console.WriteLine($"Повышено оценок    : {up,2}");
//        Console.WriteLine($"Понижено оценок    : {down,2}");
//        Console.WriteLine($"Без изменений      : {same,2}");
//        Console.WriteLine($"Всего изменено     : {up + down + same,2}");
//    }
//}

//15
//using System;
//using System.Data;
//using System.Linq;

//class Program
//{
//    static DataSet ds;
//    static DataTable customers, orders, orderDetails;
//    static DataRelation relCustOrders, relOrderDetails;

//    static void Main()
//    {
//        ds = new DataSet("Заказы");

//        // === ТАБЛИЦЫ ===
//        customers = new DataTable("Заказчики");
//        customers.Columns.Add("CustomerID", typeof(int));
//        customers.Columns.Add("Name", typeof(string));
//        customers.Columns.Add("Email", typeof(string));
//        customers.PrimaryKey = new[] { customers.Columns["CustomerID"] };

//        orders = new DataTable("Заказы");
//        orders.Columns.Add("OrderID", typeof(int));
//        orders.Columns.Add("CustomerID", typeof(int));
//        orders.Columns.Add("OrderDate", typeof(DateTime));
//        orders.Columns.Add("Total", typeof(decimal));
//        orders.PrimaryKey = new[] { orders.Columns["OrderID"] };

//        orderDetails = new DataTable("ДеталиЗаказа");
//        orderDetails.Columns.Add("DetailID", typeof(int));
//        orderDetails.Columns.Add("OrderID", typeof(int));
//        orderDetails.Columns.Add("Product", typeof(string));
//        orderDetails.Columns.Add("Quantity", typeof(int));
//        orderDetails.Columns.Add("Price", typeof(decimal));
//        orderDetails.PrimaryKey = new[] { orderDetails.Columns["DetailID"] };

//        ds.Tables.AddRange(new[] { customers, orders, orderDetails });

//        // === КАСКАДНЫЕ ОТНОШЕНИЯ ===
//        relCustOrders = new DataRelation("Cust_Orders",
//            customers.Columns["CustomerID"], orders.Columns["CustomerID"], true);
//        relCustOrders.ChildKeyConstraint.DeleteRule = Rule.Cascade;

//        relOrderDetails = new DataRelation("Order_Details",
//            orders.Columns["OrderID"], orderDetails.Columns["OrderID"], true);
//        relOrderDetails.ChildKeyConstraint.DeleteRule = Rule.Cascade;

//        ds.Relations.AddRange(new[] { relCustOrders, relOrderDetails });

//        FillData();
//        ds.AcceptChanges(); // фиксируем как "оригинал"

//        Console.WriteLine("КАСКАДНОЕ УДАЛЕНИЕ С ОТСЛЕЖИВАНИЕМ RowState.Deleted\n");

//        PrintState("ДО УДАЛЕНИЯ");

//        // Удаляем заказчика ID=2 (Петрова)
//        DeleteCustomer(2);

//        PrintCascadeDeletionReport();

//        Console.WriteLine("\nОТКАТ ВСЕХ ИЗМЕНЕНИЙ (RejectChanges)...");
//        ds.RejectChanges();

//        PrintState("ПОСЛЕ ОТКАТА");

//        Console.WriteLine("\nГотово.");
//        Console.ReadKey();
//    }

//    static void FillData()
//    {
//        customers.Rows.Add(1, "Иванов Иван", "ivanov@email.com");
//        customers.Rows.Add(2, "Петрова Анна", "petrova@email.com");
//        customers.Rows.Add(3, "Сидоров Пётр", "sidorov@email.com");

//        orders.Rows.Add(101, 1, new DateTime(2025, 1, 10), 15000m);
//        orders.Rows.Add(102, 1, new DateTime(2025, 1, 15), 8000m);
//        orders.Rows.Add(103, 2, new DateTime(2025, 2, 1), 12000m);
//        orders.Rows.Add(104, 2, new DateTime(2025, 2, 5), 5000m);

//        orderDetails.Rows.Add(1, 101, "Ноутбук", 1, 80000m);
//        orderDetails.Rows.Add(2, 101, "Мышь", 2, 1500m);
//        orderDetails.Rows.Add(3, 102, "Монитор", 1, 35000m);
//        orderDetails.Rows.Add(4, 103, "Клавиатура", 3, 3000m);
//        orderDetails.Rows.Add(5, 103, "Наушники", 1, 5000m);
//        orderDetails.Rows.Add(6, 104, "Веб-камера", 1, 4000m);
//    }

//    static void DeleteCustomer(int id)
//    {
//        var cust = customers.Rows.Find(id);
//        if (cust != null)
//        {
//            Console.WriteLine($"УДАЛЕНИЕ заказчика: {cust["Name"]} (ID={id}) → каскадное удаление заказов и деталей...");
//            cust.Delete();
//        }
//        else
//            Console.WriteLine($"Заказчик ID={id} не найден.");
//    }

//    static void PrintCascadeDeletionReport()
//    {
//        Console.WriteLine("\nОТЧЁТ: КАСКАДНОЕ УДАЛЕНИЕ (строки со статусом Deleted)\n");

//        var deletedCustomers = customers.Select(null, null, DataViewRowState.Deleted);
//        var deletedOrders = orders.Select(null, null, DataViewRowState.Deleted);
//        var deletedDetails = orderDetails.Select(null, null, DataViewRowState.Deleted);

//        if (!deletedCustomers.Any() && !deletedOrders.Any() && !deletedDetails.Any())
//        {
//            Console.WriteLine("    Нет удалённых записей.\n");
//            return;
//        }

//        foreach (DataRow cust in deletedCustomers)
//        {
//            int custId = (int)cust["CustomerID", DataRowVersion.Original];
//            string name = cust["Name", DataRowVersion.Original].ToString();

//            Console.WriteLine($"ЗАКАЗЧИК [УДАЛЁН]: {name} (ID={custId})");

//            var childOrders = cust.GetChildRows(relCustOrders, DataRowVersion.Original);
//            foreach (DataRow ord in childOrders)
//            {
//                int ordId = (int)ord["OrderID", DataRowVersion.Original];
//                decimal total = (decimal)ord["Total", DataRowVersion.Original];

//                Console.WriteLine($"    → ЗАКАЗ [УДАЛЁН]: ID={ordId}, Сумма={total:C}");

//                var details = ord.GetChildRows(relOrderDetails, DataRowVersion.Original);
//                foreach (DataRow det in details)
//                {
//                    string product = det["Product", DataRowVersion.Original].ToString();
//                    int qty = (int)det["Quantity", DataRowVersion.Original];
//                    decimal price = (decimal)det["Price", DataRowVersion.Original];

//                    Console.WriteLine($"        • ДЕТАЛЬ [УДАЛЁНА]: {product} × {qty} = {price * qty:C}");
//                }
//            }
//        }

//        Console.WriteLine($"\nИТОГО УДАЛЕНО:");
//        Console.WriteLine($"  Заказчиков : {deletedCustomers.Length}");
//        Console.WriteLine($"  Заказов    : {deletedOrders.Length}");
//        Console.WriteLine($"  Деталей    : {deletedDetails.Length}");
//        Console.WriteLine($"  Всего строк: {deletedCustomers.Length + deletedOrders.Length + deletedDetails.Length}\n");
//    }

//    static void PrintState(string title)
//    {
//        Console.WriteLine(new string('═', 80));
//        Console.WriteLine(title);
//        Console.WriteLine(new string('═', 80));

//        Console.WriteLine("ЗАКАЗЧИКИ:");
//        foreach (DataRow c in customers.Rows)
//            Console.WriteLine(c.RowState == DataRowState.Deleted
//                ? $"  [УДАЛЁН] {c["Name", DataRowVersion.Original]}"
//                : $"  {c["Name"]}");

//        Console.WriteLine("\nЗАКАЗЫ:");
//        foreach (DataRow o in orders.Rows)
//        {
//            if (o.RowState == DataRowState.Deleted)
//                Console.WriteLine($"  [УДАЛЁН] ID={o["OrderID", DataRowVersion.Original]}");
//            else
//            {
//                var cust = o.GetParentRow(relCustOrders);
//                string custName = cust != null ? cust["Name"].ToString() : "—";
//                Console.WriteLine($"  ID={o["OrderID"]} → {custName}, {o["Total"]:C}");
//            }
//        }

//        Console.WriteLine("\nДЕТАЛИ:");
//        foreach (DataRow d in orderDetails.Rows)
//        {
//            if (d.RowState == DataRowState.Deleted)
//                Console.WriteLine($"  [УДАЛЁНА] {d["Product", DataRowVersion.Original]}");
//            else
//            {
//                var ord = d.GetParentRow(relOrderDetails);
//                int ordId = ord != null ? (int)ord["OrderID"] : 0;
//                Console.WriteLine($"  {d["Product"]} × {d["Quantity"]} → Заказ {ordId}");
//            }
//        }
//        Console.WriteLine();
//    }
//}

//16
//using System;
//using System.Data;
//using System.Linq;

//class Program
//{
//    static DataSet ds;
//    static DataTable categories, products;
//    static DataRelation relation;

//    static void Main()
//    {
//        ds = new DataSet("Магазин");

//        categories = new DataTable("Категории");
//        categories.Columns.Add("CategoryID", typeof(int));
//        categories.Columns.Add("CategoryName", typeof(string));
//        categories.PrimaryKey = new[] { categories.Columns["CategoryID"] };

//        products = new DataTable("Товары");
//        products.Columns.Add("ProductID", typeof(int));
//        products.Columns.Add("ProductName", typeof(string));
//        products.Columns.Add("Price", typeof(decimal));
//        products.Columns.Add("CategoryID", typeof(int)).AllowDBNull = true;
//        products.PrimaryKey = new[] { products.Columns["ProductID"] };

//        ds.Tables.AddRange(new[] { categories, products });

//        // Отношение с включёнными ограничениями (по умолчанию createConstraints = true)
//        relation = new DataRelation("Category_Products",
//            categories.Columns["CategoryID"],
//            products.Columns["CategoryID"],
//            createConstraints: true);  // <-- Важно! Создаёт ForeignKeyConstraint

//        // Можно явно настроить правила (для примера)
//        relation.ChildKeyConstraint.DeleteRule = Rule.SetNull;
//        relation.ChildKeyConstraint.UpdateRule = Rule.Cascade;

//        ds.Relations.Add(relation);

//        FillValidData();
//        ds.AcceptChanges();

//        Console.WriteLine("ВАЛИДАЦИЯ ССЫЛОЧНОЙ ЦЕЛОСТНОСТИ ПЕРЕД СОХРАНЕНИЕМ\n");

//        // Вносим нарушения
//        IntroduceViolations();

//        PrintCurrentState("СОСТОЯНИЕ С НАРУШЕНИЯМИ");

//        var report = CheckReferentialIntegrity();

//        if (report.HasErrors)
//        {
//            Console.WriteLine("\nОБНАРУЖЕНЫ НАРУШЕНИЯ ЦЕЛОСТНОСТИ!");
//            Console.WriteLine("Автоматическое исправление...\n");
//            AutoFixIntegrity(report);
//        }

//        PrintCurrentState("ПОСЛЕ АВТОМАТИЧЕСКОГО ИСПРАВЛЕНИЯ");

//        Console.WriteLine("\nГотово. Теперь данные можно безопасно сохранять в БД.");
//        Console.ReadKey();
//    }

//    static void FillValidData()
//    {
//        categories.Rows.Add(1, "Напитки");
//        categories.Rows.Add(2, "Электроника");
//        categories.Rows.Add(3, "Продукты");

//        products.Rows.Add(1, "Кола", 85m, 1);
//        products.Rows.Add(2, "Смартфон", 89990m, 2);
//        products.Rows.Add(3, "Хлеб", 65m, 3);
//    }

//    static void IntroduceViolations()
//    {
//        // 1. Товар с несуществующей категорией (orphaned)
//        products.Rows.Add(10, "Товар-сирота", 999m, 99);

//        // 2. Товар с NULL в CategoryID (допустимо, если AllowDBNull = true)
//        products.Rows.Add(11, "Товар без категории", 500m, DBNull.Value);

//        // 3. Изменяем существующую запись на несуществующую категорию
//        var p = products.Rows.Find(2);
//        if (p != null) p["CategoryID"] = 999; // теперь тоже orphaned

//        // 4. Добавляем ещё один с NULL
//        products.Rows.Add(12, "Ещё один без категории", 300m, DBNull.Value);
//    }

//    static IntegrityReport CheckReferentialIntegrity()
//    {
//        var report = new IntegrityReport();

//        Console.WriteLine("ПРОВЕРКА ССЫЛОЧНОЙ ЦЕЛОСТНОСТИ\n");
//        Console.WriteLine(new string('═', 80));

//        foreach (DataRelation rel in ds.Relations)
//        {
//            var parentTable = rel.ParentTable;
//            var childTable = rel.ChildTable;
//            var childCol = rel.ChildColumns[0];
//            var parentCol = rel.ParentColumns[0];

//            Console.WriteLine($"Отношение: {parentTable.TableName} → {childTable.TableName} " +
//                              $"(по полю {childCol.ColumnName})");

//            var orphaned = childTable.AsEnumerable()
//                .Where(row => row.RowState != DataRowState.Deleted &&
//                             !row.IsNull(childCol) &&
//                             parentTable.Rows.Find(row[childCol]) == null)
//                .ToList();

//            var nullRefs = childTable.AsEnumerable()
//                .Where(row => row.RowState != DataRowState.Deleted &&
//                             row.IsNull(childCol))
//                .ToList();

//            if (orphaned.Any())
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine($"  НАРУШЕНИЕ: Найдено {orphaned.Count} orphaned записей (ссылаются на несуществующий родитель):");
//                foreach (var row in orphaned)
//                {
//                    report.OrphanedRows.Add(row);
//                    Console.WriteLine($"    → Товар ID={row["ProductID"]} \"{row["ProductName"]}\" → CategoryID={row["CategoryID"]} (не существует!)");
//                }
//                Console.ResetColor();
//            }
//            else
//            {
//                Console.WriteLine("  Orphaned записи: отсутствуют");
//            }

//            if (nullRefs.Any())
//            {
//                Console.ForegroundColor = ConsoleColor.Yellow;
//                Console.WriteLine($"  ПРЕДУПРЕЖДЕНИЕ: {nullRefs.Count} записей с NULL в {childCol.ColumnName} (допустимо, если разрешено):");
//                foreach (var row in nullRefs)
//                {
//                    report.NullRefRows.Add(row);
//                    Console.WriteLine($"    → Товар ID={row["ProductID"]} \"{row["ProductName"]}\" — без категории");
//                }
//                Console.ResetColor();
//            }
//            else
//            {
//                Console.WriteLine("  NULL-ссылки: отсутствуют");
//            }
//        }

//        report.HasErrors = report.OrphanedRows.Any();
//        Console.WriteLine(new string('═', 80));

//        if (!report.HasErrors)
//            Console.WriteLine("ЦЕЛОСТНОСТЬ ДАННЫХ ПОДТВЕРЖДЕНА — можно сохранять в БД.");
//        else
//            Console.WriteLine("ОБНАРУЖЕНЫ КРИТИЧЕСКИЕ НАРУШЕНИЯ — сохранение в БД небезопасно!");

//        Console.WriteLine();
//        return report;
//    }

//    static void AutoFixIntegrity(IntegrityReport report)
//    {
//        int fixedCount = 0;

//        foreach (DataRow row in report.OrphanedRows.ToList())
//        {
//            Console.WriteLine($"Исправление: Товар \"{row["ProductName"]}\" (ID={row["ProductID"]}) — устанавливаем CategoryID = NULL");
//            row["CategoryID"] = DBNull.Value;
//            fixedCount++;
//        }

//        Console.WriteLine($"\nИсправлено {fixedCount} нарушений (установлено NULL вместо несуществующей категории).");
//    }

//    static void PrintCurrentState(string title)
//    {
//        Console.WriteLine(new string('═', 80));
//        Console.WriteLine(title);
//        Console.WriteLine(new string('═', 80));

//        Console.WriteLine("КАТЕГОРИИ:");
//        foreach (DataRow c in categories.Rows)
//            Console.WriteLine($"  [{c["CategoryID"]}] {c["CategoryName"]}");

//        Console.WriteLine("\nТОВАРЫ:");
//        foreach (DataRow p in products.Rows)
//        {
//            int? catId = p.IsNull("CategoryID") ? (int?)null : (int)p["CategoryID"];
//            string catName = catId.HasValue ? categories.Rows.Find(catId.Value)?["CategoryName"]?.ToString() ?? "[НЕИЗВЕСТНО]" : "—";
//            string state = p.RowState == DataRowState.Added ? " [НОВЫЙ]" :
//                          p.RowState == DataRowState.Modified ? " [ИЗМЕНЁН]" : "";
//            Console.WriteLine($"  [{p["ProductID"]}] {p["ProductName"],-25} | Цена: {p["Price"],8:C} | Категория: {catName} {state}");
//        }
//        Console.WriteLine();
//    }

//    class IntegrityReport
//    {
//        public bool HasErrors { get; set; }
//        public System.Collections.Generic.List<DataRow> OrphanedRows { get; } = new();
//        public System.Collections.Generic.List<DataRow> NullRefRows { get; } = new();
//    }
//}

//17
//using System;
//using System.Data;
//using System.Windows.Forms;

//namespace OrderFilteringDemo
//{
//    public class Program
//    {
//        static DataSet ds;
//        static DataTable customers, orders;
//        static DataRelation relation;

//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//            CreateData();

//            var form = new Form
//            {
//                Text = "Фильтрация заказов через DataRelation и DataView",
//                Width = 1000,
//                Height = 600,
//                StartPosition = FormStartPosition.CenterScreen
//            };

//            var grid = new DataGridView
//            {
//                Dock = DockStyle.Fill,
//                ReadOnly = true,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
//            };

//            var panel = new FlowLayoutPanel
//            {
//                Dock = DockStyle.Top,
//                Height = 120,
//                Padding = new Padding(10)
//            };

//            var cbCustomer = new ComboBox { Width = 250 };
//            var dtpDate = new DatePicker { Format = DateTimePickerFormat.Short };
//            var txtAmount = new TextBox { Width = 100, Text = "0" };
//            var btnFilter = new Button { Text = "Применить фильтр", Width = 150, Height = 30 };
//            var btnClear = new Button { Text = "Сбросить", Width = 100, Height = 30 };
//            var lblSort = new Label { Text = "Сортировка:", AutoSize = true };
//            var cbSort = new ComboBox { Width = 200 };
//            cbSort.Items.AddRange(new[] { "По дате (по убыванию)", "По сумме (по убыванию)", "По ID" });

//            panel.Controls.AddRange(new Control[]
//            {
//                new Label { Text = "Заказчик:", AutoSize = true, Height = 30 },
//                cbCustomer,
//                new Label { Text = "После даты:", AutoSize = true, Height = 30 },
//                dtpDate,
//                new Label { Text = "Сумма больше:", AutoSize = true, Height = 30 },
//                txtAmount,
//                btnFilter,
//                btnClear,
//                lblSort,
//                cbSort
//            });

//            cbCustomer.Items.Add("Все заказчики");
//            foreach (DataRow c in customers.Rows)
//                cbCustomer.Items.Add($"{c["Name"]} (ID={c["CustomerID"]})");
//            cbCustomer.SelectedIndex = 0;
//            cbSort.SelectedIndex = 0;

//            btnFilter.Click += (s, e) => ApplyFilter(grid, cbCustomer, dtpDate, txtAmount, cbSort);
//            btnClear.Click += (s, e) =>
//            {
//                cbCustomer.SelectedIndex = 0;
//                dtpDate.Value = DateTime.Today.AddYears(-10);
//                txtAmount.Text = "0";
//                ApplyFilter(grid, cbCustomer, dtpDate, txtAmount, cbSort);
//            };

//            form.Controls.Add(grid);
//            form.Controls.Add(panel);

//            ApplyFilter(grid, cbCustomer, dtpDate, txtAmount, cbSort); 

//            Application.Run(form);
//        }

//        static void CreateData()
//        {
//            ds = new DataSet();

//            customers = new DataTable("Заказчики");
//            customers.Columns.Add("CustomerID", typeof(int));
//            customers.Columns.Add("Name", typeof(string));
//            customers.Columns.Add("Email", typeof(string));
//            customers.PrimaryKey = new[] { customers.Columns["CustomerID"] };

//            orders = new DataTable("Заказы");
//            orders.Columns.Add("OrderID", typeof(int));
//            orders.Columns.Add("CustomerID", typeof(int));
//            orders.Columns.Add("OrderDate", typeof(DateTime));
//            orders.Columns.Add("Total", typeof(decimal));
//            orders.Columns.Add("Status", typeof(string));
//            orders.PrimaryKey = new[] { orders.Columns["OrderID"] };

//            ds.Tables.AddRange(new[] { customers, orders });

//            relation = new DataRelation("Customer_Orders",
//                customers.Columns["CustomerID"],
//                orders.Columns["CustomerID"],
//                createConstraints: false);

//            ds.Relations.Add(relation);

//            customers.Rows.Add(1, "Иванов Иван", "ivanov@email.com");
//            customers.Rows.Add(2, "Петрова Анна", "petrova@email.com");
//            customers.Rows.Add(3, "Сидоров Пётр", "sidorov@email.com");

//            orders.Rows.Add(101, 1, new DateTime(2025, 1, 10), 15000m, "Выполнен");
//            orders.Rows.Add(102, 1, new DateTime(2025, 2, 15), 8900m, "Выполнен");
//            orders.Rows.Add(103, 2, new DateTime(2025, 3, 5), 25000m, "В обработке");
//            orders.Rows.Add(104, 2, new DateTime(2025, 1, 20), 5600m, "Выполнен");
//            orders.Rows.Add(105, 3, new DateTime(2025, 4, 1), 32000m, "Выполнен");
//            orders.Rows.Add(106, 1, new DateTime(2025, 4, 12), 12500m, "В обработке");
//            orders.Rows.Add(107, 2, new DateTime(2025, 5, 1), 18000m, "Выполнен");
//        }

//        static void ApplyFilter(DataGridView grid, ComboBox cbCust, DatePicker dtp, TextBox txtAmt, ComboBox cbSort)
//        {
//            int? selectedCustomerId = null;
//            if (cbCust.SelectedIndex > 0)
//            {
//                string text = cbCust.SelectedItem.ToString();
//                var parts = text.Split('=');
//                selectedCustomerId = int.Parse(parts[1].TrimEnd(')'));
//            }

//            DateTime filterDate = dtp.Value;
//            decimal minAmount = decimal.TryParse(txtAmt.Text, out var amt) ? amt : 0;

//            var view = new DataView(orders);

//            string filter = "1=1"; 

//            if (selectedCustomerId.HasValue)
//                filter += $" AND CustomerID = {selectedCustomerId.Value}";

//            if (filterDate > DateTime.MinValue)
//                filter += $" AND OrderDate >= #{filterDate:yyyy/MM/dd}#";

//            if (minAmount > 0)
//                filter += $" AND Total > {minAmount}";

//            view.RowFilter = filter;

//            string sort = cbSort.SelectedIndex switch
//            {
//                0 => "OrderDate DESC",
//                1 => "Total DESC",
//                _ => "OrderID"
//            };
//            view.Sort = sort;

//            grid.DataSource = view;

//            if (selectedCustomerId.HasValue)
//            {
//                var customerRow = customers.Rows.Find(selectedCustomerId.Value);
//                if (customerRow != null)
//                {
//                    var childOrders = customerRow.GetChildRows(relation);
//                    Console.WriteLine($"\nЧерез GetChildRows(): найдено {childOrders.Length} заказов у {customerRow["Name"]}");
//                }
//            }

//            if (grid.Columns.Count > 0)
//            {
//                grid.Columns["CustomerID"].Visible = false;
//                grid.Columns["OrderID"].HeaderText = "Номер заказа";
//                grid.Columns["OrderDate"].HeaderText = "Дата";
//                grid.Columns["Total"].HeaderText = "Сумма";
//                grid.Columns["Status"].HeaderText = "Статус";

//                grid.Columns["Total"].DefaultCellStyle.Format = "C";
//                grid.Columns["OrderDate"].DefaultCellStyle.Format = "dd.MM.yyyy";
//            }

//            grid.Columns["OrderID"].FillWeight = 20;
//            grid.Columns["OrderDate"].FillWeight = 25;
//            grid.Columns["Total"].FillWeight = 25;
//            grid.Columns["Status"].FillWeight = 30;
//        }
//    }
//}

//18
//using System;
//using System.Data;
//using System.Linq;
//using System.Windows.Forms;

//namespace UniversityManagementSystem
//{
//    public partial class MainForm : Form
//    {
//        private DataSet ds;
//        private DataRelation relFacultySpec, relSpecStudent, relStudentGrade, relSubjectGrade;

//        public MainForm()
//        {
//            InitializeComponent();
//            InitializeDataSet();
//            FillTestData();
//            SetupBindings();
//            RefreshAll();
//        }

//        private void InitializeDataSet()
//        {
//            ds = new DataSet("Университет");

//            var faculties = new DataTable("Факультеты");
//            faculties.Columns.Add("FacultyID", typeof(int));
//            faculties.Columns.Add("FacultyName", typeof(string));
//            faculties.PrimaryKey = new[] { faculties.Columns["FacultyID"] };

//            var specialties = new DataTable("Специальности");
//            specialties.Columns.Add("SpecialtyID", typeof(int));
//            specialties.Columns.Add("SpecialtyName", typeof(string));
//            specialties.Columns.Add("FacultyID", typeof(int));
//            specialties.PrimaryKey = new[] { specialties.Columns["SpecialtyID"] };

//            var students = new DataTable("Студенты");
//            students.Columns.Add("StudentID", typeof(int));
//            students.Columns.Add("StudentName", typeof(string));
//            students.Columns.Add("SpecialtyID", typeof(int)).AllowDBNull = true;
//            students.PrimaryKey = new[] { students.Columns["StudentID"] };

//            var subjects = new DataTable("Предметы");
//            subjects.Columns.Add("SubjectID", typeof(int));
//            subjects.Columns.Add("SubjectName", typeof(string));
//            subjects.PrimaryKey = new[] { subjects.Columns["SubjectID"] };

//            var grades = new DataTable("Оценки");
//            grades.Columns.Add("GradeID", typeof(int));
//            grades.Columns.Add("StudentID", typeof(int));
//            grades.Columns.Add("SubjectID", typeof(int));
//            grades.Columns.Add("Grade", typeof(int));
//            grades.Columns.Add("Date", typeof(DateTime));
//            grades.PrimaryKey = new[] { grades.Columns["GradeID"] };

//            ds.Tables.AddRange(new[] { faculties, specialties, students, subjects, grades });

//            relFacultySpec = new DataRelation("Faculty_Specialties",
//                faculties.Columns["FacultyID"], specialties.Columns["FacultyID"], true);
//            relFacultySpec.ChildKeyConstraint.DeleteRule = Rule.Cascade;
//            relFacultySpec.ChildKeyConstraint.UpdateRule = Rule.Cascade;

//            relSpecStudent = new DataRelation("Specialty_Students",
//                specialties.Columns["SpecialtyID"], students.Columns["SpecialtyID"], true);
//            relSpecStudent.ChildKeyConstraint.DeleteRule = Rule.SetNull;
//            relSpecStudent.ChildKeyConstraint.UpdateRule = Rule.Cascade;

//            relStudentGrade = new DataRelation("Student_Grades",
//                students.Columns["StudentID"], grades.Columns["StudentID"], false);

//            relSubjectGrade = new DataRelation("Subject_Grades",
//                subjects.Columns["SubjectID"], grades.Columns["SubjectID"], false);

//            ds.Relations.AddRange(new[] { relFacultySpec, relSpecStudent, relStudentGrade, relSubjectGrade });
//        }

//        private void FillTestData()
//        {
//            var f = ds.Tables["Факультеты"];
//            var s = ds.Tables["Специальности"];
//            var st = ds.Tables["Студенты"];
//            var sub = ds.Tables["Предметы"];
//            var g = ds.Tables["Оценки"];

//            f.Rows.Add(1, "Информационных технологий");
//            f.Rows.Add(2, "Экономический");

//            s.Rows.Add(101, "Программная инженерия", 1);
//            s.Rows.Add(102, "Информационная безопасность", 1);
//            s.Rows.Add(201, "Финансы и кредит", 2);

//            st.Rows.Add(1001, "Иванов Иван", 101);
//            st.Rows.Add(1002, "Петров Пётр", 101);
//            st.Rows.Add(1003, "Сидорова Анна", 102);
//            st.Rows.Add(2001, "Козлова Мария", 201);

//            sub.Rows.Add(1, "Математика");
//            sub.Rows.Add(2, "Программирование");
//            sub.Rows.Add(3, "Базы данных");
//            sub.Rows.Add(4, "Экономика");

//            g.Rows.Add(1, 1001, 1, 5, new DateTime(2025, 1, 15));
//            g.Rows.Add(2, 1001, 2, 4, new DateTime(2025, 2, 10));
//            g.Rows.Add(3, 1002, 2, 5, new DateTime(2025, 2, 12));
//            g.Rows.Add(4, 1003, 3, 5, new DateTime(2025, 3, 1));
//            g.Rows.Add(5, 2001, 4, 4, new DateTime(2025, 1, 20));

//            ds.AcceptChanges();
//        }

//        private void SetupBindings()
//        {
//            dgvFaculties.DataSource = ds.Tables["Факультеты"];
//            dgvSpecialties.DataSource = ds.Tables["Специальности"];
//            dgvStudents.DataSource = ds.Tables["Студенты"];
//            dgvSubjects.DataSource = ds.Tables["Предметы"];
//            dgvGrades.DataSource = ds.Tables["Оценки"];
//        }

//        private void btnDeleteFaculty_Click(object sender, EventArgs e)
//        {
//            if (dgvFaculties.CurrentRow == null) return;
//            var row = (DataRowView)dgvFaculties.CurrentRow.DataBoundItem;
//            row.Row.Delete();
//            RefreshAll();
//        }

//        private void btnShowFacultyReport_Click(object sender, EventArgs e)
//        {
//            var report = "ОТЧЁТ ПО ФАКУЛЬТЕТАМ\n\n";
//            foreach (DataRow f in ds.Tables["Факультеты"].Rows)
//            {
//                if (f.RowState == DataRowState.Deleted) continue;
//                var specs = f.GetChildRows(relFacultySpec);
//                var students = specs.SelectMany(sp => sp.GetChildRows(relSpecStudent));
//                var avg = students.Any() ? students.Average(stu => GetStudentAverage(stu)) : 0;

//                report += $"{f["FacultyName"]}\n";
//                report += $"  Специальностей: {specs.Length}\n";
//                report += $"  Студентов: {students.Length}\n";
//                report += $"  Средний балл: {avg:F2}\n\n";
//            }
//            txtReport.Text = report;
//        }

//        private void btnShowStudentReport_Click(object sender, EventArgs e)
//        {
//            if (dgvStudents.CurrentRow == null) return;
//            var student = (DataRowView)dgvStudents.CurrentRow.DataBoundItem;
//            var row = student.Row;

//            var report = $"СТУДЕНТ: {row["StudentName"]}\n";
//            var specRow = row.GetParentRow(relSpecStudent);
//            report += $"Специальность: {(specRow != null ? specRow["SpecialtyName"] : "—")}\n";

//            var grades = row.GetChildRows(relStudentGrade);
//            if (grades.Any())
//            {
//                report += "ОЦЕНКИ:\n";
//                foreach (var g in grades)
//                {
//                    var subj = g.GetParentRow(relSubjectGrade);
//                    report += $"  {subj["SubjectName"]}: {g["Grade"]} ({g["Date"]:dd.MM.yyyy})\n";
//                }
//                report += $"СРЕДНИЙ БАЛЛ: {GetStudentAverage(row):F2}\n";
//            }
//            else
//            {
//                report += "Оценок нет\n";
//            }

//            txtReport.Text = report;
//        }

//        private double GetStudentAverage(DataRow student)
//        {
//            var grades = student.GetChildRows(relStudentGrade)
//                .Where(g => g.RowState != DataRowState.Deleted)
//                .Select(g => (int)g["Grade"]);
//            return grades.Any() ? grades.Average() : 0;
//        }

//        private void btnChangesReport_Click(object sender, EventArgs e)
//        {
//            var added = ds.Tables.Cast<DataTable>().Sum(t => t.Select(null, null, DataViewRowState.Added).Length);
//            var modified = ds.Tables.Cast<DataTable>().Sum(t => t.Select(null, null, DataViewRowState.Modified).Length);
//            var deleted = ds.Tables.Cast<DataTable>().Sum(t => t.Select(null, null, DataViewRowState.Deleted).Length);

//            var report = "ИЗМЕНЕНИЯ В ДАННЫХ\n\n";
//            report += $"Добавлено записей: {added}\n";
//            report += $"Изменено записей: {modified}\n";
//            report += $"Удалено записей: {deleted}\n\n";

//            if (CheckIntegrity())
//                report += "Целостность данных подтверждена\n";
//            else
//                report += "ОБНАРУЖЕНЫ НАРУШЕНИЯ ЦЕЛОСТНОСТИ!\n";

//            txtReport.Text = report;
//        }

//        private bool CheckIntegrity()
//        {
//            foreach (DataRow row in ds.Tables["Студенты"].Rows)
//            {
//                if (row.RowState == DataRowState.Deleted) continue;
//                if (!row.IsNull("SpecialtyID"))
//                {
//                    var specId = (int)row["SpecialtyID"];
//                    if (ds.Tables["Специальности"].Rows.Find(specId) == null)
//                        return false;
//                }
//            }
//            return true;
//        }

//        private void RefreshAll()
//        {
//            dgvFaculties.Refresh();
//            dgvSpecialties.Refresh();
//            dgvStudents.Refresh();
//            dgvGrades.Refresh();
//        }

//        private void InitializeComponent()
//        {
//            Text = "Управление университетом — DataSet";
//            Width = 1200;
//            Height = 700;
//            StartPosition = FormStartPosition.CenterScreen;

//            var splitMain = new SplitContainer { Dock = DockStyle.Fill };
//            var splitLeft = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical };
//            var panelRight = new Panel { Dock = DockStyle.Fill };

//            dgvFaculties = new DataGridView { Dock = DockStyle.Fill };
//            dgvSpecialties = new DataGridView { Dock = DockStyle.Fill };
//            dgvStudents = new DataGridView { Dock = DockStyle.Fill };
//            dgvSubjects = new DataGridView { Dock = DockStyle.Fill };
//            dgvGrades = new DataGridView { Dock = DockStyle.Fill };

//            var tab = new TabControl { Dock = DockStyle.Fill };
//            tab.TabPages.Add("Факультеты", dgvFaculties);
//            tab.TabPages.Add("Специальности", dgvSpecialties);
//            tab.TabPages.Add("Студенты", dgvStudents);
//            tab.TabPages.Add("Предметы", dgvSubjects);
//            tab.TabPages.Add("Оценки", dgvGrades);

//            splitLeft.Panel1.Controls.Add(tab);
//            splitLeft.Panel2.Controls.Add(panelRight);
//            splitLeft.SplitterDistance = 800;

//            var btnDeleteFaculty = new Button { Text = "Удалить факультет", Top = 10, Left = 10, Width = 150 };
//            var btnFacultyReport = new Button { Text = "Отчёт по факультетам", Top = 50, Left = 10, Width = 150 };
//            var btnStudentReport = new Button { Text = "Отчёт по студенту", Top = 90, Left = 10, Width = 150 };
//            var btnChanges = new Button { Text = "Изменения", Top = 130, Left = 10, Width = 150 };

//            txtReport = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, Font = new System.Drawing.Font("Consolas", 10) };

//            btnDeleteFaculty.Click += btnDeleteFaculty_Click;
//            btnFacultyReport.Click += btnShowFacultyReport_Click;
//            btnStudentReport.Click += btnShowStudentReport_Click;
//            btnChanges.Click += btnChangesReport_Click;

//            var panelButtons = new Panel { Height = 200, Dock = DockStyle.Top };
//            panelButtons.Controls.AddRange(new Control[] { btnDeleteFaculty, btnFacultyReport, btnStudentReport, btnChanges });

//            panelRight.Controls.Add(txtReport);
//            panelRight.Controls.Add(panelButtons);
//            panelButtons.SendToBack();

//            splitMain.Panel1.Controls.Add(splitLeft);
//            splitMain.Panel2.Controls.Add(panelRight);
//            splitMain.SplitterDistance = 900;

//            Controls.Add(splitMain);
//        }

//        private DataGridView dgvFaculties, dgvSpecialties, dgvStudents, dgvSubjects, dgvGrades;
//        private TextBox txtReport;
//    }

//    static class Program
//    {
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new MainForm());
//        }
//    }
//}

//19
//using System;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;

//namespace DataRelationPerformanceTest
//{
//    class Program
//    {
//        static DataSet ds;
//        static DataTable companies, departments, employees;
//        static DataRelation relCompDept, relDeptEmp;

//        const int COMPANY_COUNT = 10_000;
//        const int DEPT_COUNT = 100_000;
//        const int EMP_COUNT = 1_000_000;

//        static void Main()
//        {
//            Console.WriteLine("ЗАГРУЗКА 1,000,000+ ЗАПИСЕЙ В DataSet...");
//            CreateLargeDataSet();

//            Console.WriteLine("СРАВНЕНИЕ ПРОИЗВОДИТЕЛЬНОСТИ ПОДХОДОВ\n");
//            Console.WriteLine(new string('═', 80));

//            var results = new StringBuilder();
//            results.AppendLine("ПОДХОД".PadRight(30) + "ВРЕМЯ (мс)".PadRight(15) + "ПАМЯТЬ (МБ)".PadRight(15) + "ПРИМЕЧАНИЕ");
//            results.AppendLine(new string('─', 80));

//            long memoryBefore = GC.GetTotalMemory(true);

//            var time1 = Measure(() => GetEmployeesByCompany_GetChildRows(1));
//            var mem1 = GetMemoryUsage(memoryBefore);
//            results.AppendLine($"GetChildRows() в цикле".PadRight(30) + $"{time1,10:F0} мс".PadRight(15) + $"{mem1,10:F1}".PadRight(15) + "Очень медленно");

//            var time2 = Measure(() => GetEmployeesByCompany_DataView(1));
//            var mem2 = GetMemoryUsage(memoryBefore);
//            results.AppendLine($"DataView + RowFilter".PadRight(30) + $"{time2,10:F0} мс".PadRight(15) + $"{mem2,10:F1}".PadRight(15) + "Быстро");

//            var time3 = Measure(() => GetEmployeesByCompany_Linq(1));
//            var mem3 = GetMemoryUsage(memoryBefore);
//            results.AppendLine($"LINQ to DataSet".PadRight(30) + $"{time3,10:F0} мс".PadRight(15) + $"{mem3,10:F1}".PadRight(15) + "Средне");

//            var time4 = Measure(() => GetEmployeesByCompany_Dictionary(1));
//            var mem4 = GetMemoryUsage(memoryBefore);
//            results.AppendLine($"Словарь (как SQL)".PadRight(30) + $"{time4,10:F0} мс".PadRight(15) + $"{mem4,10:F1}".PadRight(15) + "Самый быстрый");

//            Console.WriteLine(results.ToString());

//            Console.WriteLine("\nРЕКОМЕНДАЦИИ ПО ОПТИМИЗАЦИИ ДЛЯ БОЛЬШИХ ДАННЫХ:");
//            Console.WriteLine("─────────────────────────────────────────────────────");
//            Console.WriteLine("1. Избегайте GetChildRows() в циклах — O(n×m), очень медленно");
//            Console.WriteLine("2. Используйте DataView.RowFilter — использует внутренние индексы");
//            Console.WriteLine("3. Для максимальной скорости — создавайте словари (Dictionary<int, List<DataRow>>)");
//            Console.WriteLine("4. LINQ to DataSet — удобно, но медленнее индексированного доступа");
//            Console.WriteLine("5. DataRelation полезен для малых/средних объёмов (<100k) и навигации");
//            Console.WriteLine("6. При >500k строк — переходите на Entity Framework, Dapper или словари");
//            Console.WriteLine("7. Кэшируйте результаты часто используемых запросов");

//            Console.WriteLine("\nГотово.");
//            Console.ReadKey();
//        }

//        static void CreateLargeDataSet()
//        {
//            ds = new DataSet();

//            companies = new DataTable("Компании");
//            companies.Columns.Add("CompanyID", typeof(int));
//            companies.Columns.Add("Name", typeof(string));
//            companies.PrimaryKey = new[] { companies.Columns["CompanyID"] };

//            departments = new DataTable("Отделы");
//            departments.Columns.Add("DeptID", typeof(int));
//            departments.Columns.Add("CompanyID", typeof(int));
//            departments.Columns.Add("Name", typeof(string));
//            departments.PrimaryKey = new[] { departments.Columns["DeptID"] };

//            employees = new DataTable("Сотрудники");
//            employees.Columns.Add("EmpID", typeof(int));
//            employees.Columns.Add("DeptID", typeof(int));
//            employees.Columns.Add("Name", typeof(string));
//            employees.Columns.Add("Salary", typeof(decimal));
//            employees.PrimaryKey = new[] { employees.Columns["EmpID"] };

//            ds.Tables.AddRange(new[] { companies, departments, employees });

//            relCompDept = new DataRelation("Comp_Dept", companies.Columns["CompanyID"], departments.Columns["CompanyID"], false);
//            relDeptEmp = new DataRelation("Dept_Emp", departments.Columns["DeptID"], employees.Columns["DeptID"], false);
//            ds.Relations.AddRange(new[] { relCompDept, relDeptEmp });

//            var rnd = new Random(42);
//            for (int c = 1; c <= COMPANY_COUNT; c++)
//                companies.Rows.Add(c, $"Компания {c}");

//            for (int d = 1; d <= DEPT_COUNT; d++)
//                departments.Rows.Add(d, rnd.Next(1, COMPANY_COUNT + 1), $"Отдел {d}");

//            for (int e = 1; e <= EMP_COUNT; e++)
//                employees.Rows.Add(e, rnd.Next(1, DEPT_COUNT + 1), $"Сотрудник {e}", rnd.Next(50000, 300000));

//            ds.AcceptChanges();
//            Console.WriteLine($"Создано: {COMPANY_COUNT} компаний, {DEPT_COUNT} отделов, {EMP_COUNT} сотрудников");
//        }

//        static void GetEmployeesByCompany_GetChildRows(int companyId)
//        {
//            var company = companies.Rows.Find(companyId);
//            if (company == null) return;

//            int count = 0;
//            var depts = company.GetChildRows(relCompDept);
//            foreach (var dept in depts)
//            {
//                var emps = dept.GetChildRows(relDeptEmp);
//                count += emps.Length;
//            }
//        }

//        static void GetEmployeesByCompany_DataView(int companyId)
//        {
//            var deptView = new DataView(departments) { RowFilter = $"CompanyID = {companyId}" };
//            var empView = new DataView(employees);

//            int count = 0;
//            foreach (DataRowView deptRow in deptView)
//            {
//                empView.RowFilter = $"DeptID = {deptRow["DeptID"]}";
//                count += empView.Count;
//            }
//        }

//        static void GetEmployeesByCompany_Linq(int companyId)
//        {
//            var query = from dept in departments.AsEnumerable()
//                        where dept.Field<int>("CompanyID") == companyId
//                        join emp in employees.AsEnumerable()
//                        on dept.Field<int>("DeptID") equals emp.Field<int>("DeptID")
//                        select emp;
//            var count = query.Count();
//        }

//        static readonly System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<DataRow>> DeptToEmpsCache
//            = employees.AsEnumerable()
//                .GroupBy(r => r.Field<int>("DeptID"))
//                .ToDictionary(g => g.Key, g => g.ToList());

//        static void GetEmployeesByCompany_Dictionary(int companyId)
//        {
//            var depts = departments.Select($"CompanyID = {companyId}");
//            int count = 0;
//            foreach (DataRow dept in depts)
//            {
//                if (DeptToEmpsCache.TryGetValue((int)dept["DeptID"], out var emps))
//                    count += emps.Count;
//            }
//        }

//        static long Measure(Action action)
//        {
//            GC.Collect();
//            GC.WaitForPendingFinalizers();
//            var sw = Stopwatch.StartNew();
//            action();
//            sw.Stop();
//            return sw.ElapsedMilliseconds;
//        }

//        static double GetMemoryUsage(long before)
//        {
//            GC.Collect();
//            long after = GC.GetTotalMemory(true);
//            return (after - before) / 1024.0 / 1024.0;
//        }
//    }

//}
