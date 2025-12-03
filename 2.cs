//1
//using System;
//using System.Data;

//namespace AdoNetRowStateDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.OutputEncoding = System.Text.Encoding.UTF8; // для корректного отображения кириллицы

//            DataTable dtStudents = CreateStudentsTable();

//            FillInitialData(dtStudents);

//            Console.WriteLine("=== Исходное состояние таблицы ===");
//            PrintRowStateReport(dtStudents);

//            AddNewStudents(dtStudents);

//            Console.WriteLine("\n=== После добавления 3 новых учащихся ===");
//            PrintRowStateReport(dtStudents);

//            ModifyExistingStudents(dtStudents);

//            Console.WriteLine("\n=== После изменения 2 учащихся ===");
//            PrintRowStateReport(dtStudents);

//            DeleteStudent(dtStudents);

//            Console.WriteLine("\n=== После удаления 1 учащегося ===");
//            PrintRowStateReport(dtStudents);

//            Console.WriteLine("\n=== Отчёт только по изменённым строкам (GetChanges()) ===");
//            DataTable changes = dtStudents.GetChanges();
//            if (changes != null)
//            {
//                PrintRowStateReport(changes);
//                Console.WriteLine($"Всего изменённых строк: {changes.Rows.Count}");
//            }
//            else
//            {
//                Console.WriteLine("Изменений нет.");
//            }

//            Console.WriteLine("\nНажмите любую клавишу для выхода...");
//            Console.ReadKey();
//        }

//        static DataTable CreateStudentsTable()
//        {
//            DataTable dt = new DataTable("Students");

//            dt.Columns.Add("ID", typeof(int)).AutoIncrement = true;
//            dt.Columns["ID"].AutoIncrementSeed = 1;
//            dt.Columns["ID"].AutoIncrementStep = 1;

//            dt.Columns.Add("ФИО", typeof(string));
//            dt.Columns.Add("Email", typeof(string));
//            dt.Columns.Add("Класс", typeof(string));
//            dt.Columns.Add("СредняяОценка", typeof(double));

//            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };

//            return dt;
//        }

//        static void FillInitialData(DataTable dt)
//        {
//            dt.Rows.Add(null, "Иванов Иван Иванович", "ivanov@mail.ru", "10А", 4.5);
//            dt.Rows.Add(null, "Петрова Анна Сергеевна", "petrova@mail.ru", "11Б", 4.8);
//            dt.Rows.Add(null, "Сидоров Пётр Алексеевич", "sidorov@mail.ru", "10А", 3.9);

//            dt.AcceptChanges();
//        }

//        static void AddNewStudents(DataTable dt)
//        {
//            dt.Rows.Add(null, "Козлов Дмитрий Васильевич", "kozlov@mail.ru", "11А", 4.2);
//            dt.Rows.Add(null, "Морозова Екатерина Ильинична", "morozova@mail.ru", "10Б", 4.9);
//            dt.Rows.Add(null, "Новиков Артём Денисович", "novikov@mail.ru", "11Б", 4.0);
//        }

//        static void ModifyExistingStudents(DataTable dt)
//        {
//            DataRow row1 = dt.Rows.Find(1);
//            DataRow row2 = dt.Rows.Find(2);

//            if (row1 != null)
//            {
//                row1["Email"] = "ivanov.new@mail.ru";
//                row1["СредняяОценка"] = 4.7;
//            }

//            if (row2 != null)
//            {
//                row2["Email"] = "petrova2025@mail.ru";
//                row2["СредняяОценка"] = 5.0;
//            }
//        }

//        static void DeleteStudent(DataTable dt)
//        {
//            DataRow row = dt.Rows.Find(3);
//            if (row != null)
//            {
//                row.Delete();
//            }
//        }

//        static void PrintRowStateReport(DataTable dt)
//        {
//            Console.WriteLine($"{"ID",-3} | {"Состояние",-12} | {"ФИО",-30} | {"Email",-25} | {"Класс",-5} | {"Оценка",-6} | Изменённые поля");

//            Console.WriteLine(new string('-', 110));

//            foreach (DataRow row in dt.Rows)
//            {
//                string state = row.RowState.ToString();
//                string changedFields = GetChangedFields(row);

//                string id = row["ID", DataRowVersion.Current].ToString();
//                if (row.RowState == DataRowState.Deleted)
//                {
//                    id = row["ID", DataRowVersion.Original].ToString();
//                    Console.WriteLine($"{id,-3} | {state,-12} | {"<УДАЛЕНА>",-30} | {"",-25} | {"",-5} | {"",-6} | {changedFields}");
//                }
//                else
//                {
//                    string fio = row["ФИО"].ToString();
//                    string email = row["Email"].ToString();
//                    string klass = row["Класс"].ToString();
//                    string grade = row["СредняяОценка"].ToString();

//                    Console.WriteLine($"{id,-3} | {state,-12} | {fio,-30} | {email,-25} | {klass,-5} | {grade,-6} | {changedFields}");
//                }
//            }
//            Console.WriteLine($"Всего строк в таблице: {dt.Rows.Count} (включая удалённые)\n");
//        }

//        static string GetChangedFields(DataRow row)
//        {
//            if (row.RowState != DataRowState.Modified)
//                return "-";

//            var changed = new System.Collections.Generic.List<string>();

//            foreach (DataColumn col in row.Table.Columns)
//            {
//                if (row[col, DataRowVersion.Current] != null &&
//                    row[col, DataRowVersion.Original] != null &&
//                    !row[col, DataRowVersion.Current].Equals(row[col, DataRowVersion.Original]))
//                {
//                    changed.Add(col.ColumnName);
//                }
//                else if ((row[col, DataRowVersion.Current] == null && row[col, DataRowVersion.Original] != null) ||
//                         (row[col, DataRowVersion.Current] != null && row[col, DataRowVersion.Original] == null))
//                {
//                    changed.Add(col.ColumnName);
//                }
//            }

//            return changed.Count > 0 ? string.Join(", ", changed) : "-";
//        }
//    }
//}

//2
//using System;
//using System.Data;
//using System.Collections.Generic;
//using System.Linq;

//namespace AdoNetDataRowVersionDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.OutputEncoding = System.Text.Encoding.UTF8;

//            DataTable dtProducts = CreateProductsTable();
//            FillInitialProducts(dtProducts);

//            Console.WriteLine("Исходные данные товаров (после AcceptChanges):\n");
//            PrintAllProducts(dtProducts);

//            ModifyProducts(dtProducts);

//            Console.WriteLine("\nПосле изменения цен и количества:\n");
//            PrintAllProducts(dtProducts);

//            Console.WriteLine("\nОТЧЁТ ПО ИЗМЕНЕНИЯМ ЦЕН");
//            PrintPriceChangeReport(dtProducts);

//            Console.WriteLine("\nТОВАРЫ С ИЗМЕНЁННОЙ ЦЕНОЙ");
//            PrintOnlyPriceChangedProducts(dtProducts);

//            Console.WriteLine("\nДЕМОНСТРАЦИЯ ВСЕХ ВЕРСИЙ СТРОКИ (включая Proposed)");
//            DemonstrateProposedVersion(dtProducts);

//            Console.WriteLine("\nГотово. Нажмите любую клавишу...");
//            Console.ReadKey();
//        }

//        static DataTable CreateProductsTable()
//        {
//            DataTable dt = new DataTable("Products");

//            dt.Columns.Add("ID", typeof(int)).AutoIncrement = true;
//            dt.Columns["ID"].AutoIncrementSeed = 1;
//            dt.Columns["ID"].AutoIncrementStep = 1;

//            dt.Columns.Add("Название", typeof(string));
//            dt.Columns.Add("Цена", typeof(decimal));
//            dt.Columns.Add("КоличествоНаСкладе", typeof(int));
//            dt.Columns.Add("СтатусДоступности", typeof(string));

//            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };

//            return dt;
//        }

//        static void FillInitialProducts(DataTable dt)
//        {
//            dt.Rows.Add(null, "Ноутбук Lenovo", 75000.00m, 12, "В наличии");
//            dt.Rows.Add(null, "Смартфон Xiaomi", 25000.50m, 35, "В наличии");
//            dt.Rows.Add(null, "Монитор 27\" Dell", 32000.00m, 8, "В наличии");
//            dt.Rows.Add(null, "Клавиатура Logitech", 4500.00m, 50, "В наличии");
//            dt.Rows.Add(null, "Мышь беспроводная", 1800.00m, 120, "В наличии");

//            dt.AcceptChanges();
//        }

//        static void ModifyProducts(DataTable dt)
//        {
//            ModifyProduct(dt, 1, price: 79990.00m, quantity: 10);
//            ModifyProduct(dt, 2, price: 22990.00m, quantity: 40);
//            ModifyProduct(dt, 4, price: 4990.00m);
//        }

//        static void ModifyProduct(DataTable dt, int id, decimal? price = null, int? quantity = null)
//        {
//            DataRow row = dt.Rows.Find(id);
//            if (row != null)
//            {
//                if (price.HasValue) row["Цена"] = price.Value;
//                if (quantity.HasValue) row["КоличествоНаСкладе"] = quantity.Value;
//            }
//        }

//        static void PrintAllProducts(DataTable dt)
//        {
//            Console.WriteLine($"{"ID",-3} | {"Название",-25} | {"Цена",-12} | {"Кол-во",-6} | {"Статус",-12} | Состояние");
//            Console.WriteLine(new string('-', 90));
//            foreach (DataRow row in dt.Rows)
//            {
//                if (row.RowState == DataRowState.Deleted) continue;

//                Console.WriteLine($"{row["ID"],-3} | {row["Название"],-25} | {row["Цена"],-12:N2} | " +
//                                  $"{row["КоличествоНаСкладе"],-6} | {row["СтатусДоступности"],-12} | {row.RowState}");
//            }
//            Console.WriteLine();
//        }

//        static void PrintPriceChangeReport(DataTable dt)
//        {
//            Console.WriteLine($"{"ID",-3} | {"Товар",-25} | {"Старая цена",-12} | {"Новая цена",-12} | {"Разница",-10} | {"% изменения",-12} | Состояние");

//            Console.WriteLine(new string('-', 100));

//            foreach (DataRow row in dt.Rows)
//            {
//                if (row.RowState == DataRowState.Unchanged || row.RowState == DataRowState.Detached || row.RowState == DataRowState.Deleted)
//                    continue;

//                decimal oldPrice = GetValue<decimal>(row, "Цена", DataRowVersion.Original);
//                decimal newPrice = GetValue<decimal>(row, "Цена", DataRowVersion.Current);

//                decimal diff = newPrice - oldPrice;
//                decimal percentChange = oldPrice != 0 ? (diff / oldPrice) * 100 : 0;

//                string percentStr = percentChange > 0 ? $"+{percentChange:F2}%" :
//                                   percentChange < 0 ? $"{percentChange:F2}%" : "0.00%";

//                Console.WriteLine($"{row["ID"],-3} | {row["Название"],-25} | {oldPrice,-12:N2} | " +
//                                  $"{newPrice,-12:N2} | {diff,+10:N2} | {percentStr,-12} | {row.RowState}");
//            }
//        }

//        static void PrintOnlyPriceChangedProducts(DataTable dt)
//        {
//            var changed = dt.AsEnumerable()
//                .Where(r => r.RowState == DataRowState.Modified &&
//                            HasPriceChanged(r))
//                .ToList();

//            if (!changed.Any())
//            {
//                Console.WriteLine("Нет товаров с изменённой ценой.");
//                return;
//            }

//            Console.WriteLine($"{"ID",-3} | {"Товар",-25} | {"Старая → Новая цена",-25} | {"Изменение",-15}");
//            Console.WriteLine(new string('-', 80));
//            foreach (var row in changed)
//            {
//                decimal oldP = GetValue<decimal>(row, "Цена", DataRowVersion.Original);
//                decimal newP = GetValue<decimal>(row, "Цена", DataRowVersion.Current);
//                Console.WriteLine($"{row["ID"],-3} | {row["Название"],-25} | {oldP:N2} → {newP:N2} | " +
//                                  $"{(newP > oldP ? "Повышение" : "Снижение"),-15}");
//            }
//        }

//        static bool HasPriceChanged(DataRow row)
//        {
//            try
//            {
//                return !row["Цена", DataRowVersion.Original].Equals(row["Цена", DataRowVersion.Current]);
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        static T GetValue<T>(DataRow row, string columnName, DataRowVersion version)
//        {
//            try
//            {
//                if (row.HasVersion(version))
//                    return (T)row[columnName, version];
//            }
//            catch (VersionNotFoundException)
//            {
//            }
//            return default(T);
//        }

//        static void DemonstrateProposedVersion(DataTable dt)
//        {
//            DataRow row = dt.Rows.Find(3);

//            Console.WriteLine($"До BeginEdit — RowState: {row.RowState}");

//            row.BeginEdit();
//            row["Цена"] = 34990.00m;
//            row["КоличествоНаСкладе"] = 5;

//            Console.WriteLine($"После BeginEdit и установки значений — RowState: {row.RowState}");
//            Console.WriteLine($"   Текущая цена (Current): {row["Цена", DataRowVersion.Current]:N2}");
//            Console.WriteLine($"   Исходная цена (Original): {row["Цена", DataRowVersion.Original]:N2}");
//            Console.WriteLine($"   Предлагаемая цена (Proposed): {row["Цена", DataRowVersion.Proposed]:N2}");

//            row.EndEdit();
//            Console.WriteLine($"После EndEdit — RowState: {row.RowState} (теперь Modified)");
//            Console.WriteLine($"   Proposed больше недоступна\n");

//            try
//            {
//                var proposed = row["Цена", DataRowVersion.Proposed];
//            }
//            catch (VersionNotFoundException ex)
//            {
//                Console.WriteLine($"   Ошибка: {ex.Message} — Proposed версия доступна только во время редактирования!");
//            }
//        }
//    }

//    public static class DataRowExtensions
//    {
//        public static bool HasPriceChanged(this DataRow row)
//        {
//            return row.RowState == DataRowState.Modified &&
//                   !row["Цена", DataRowVersion.Original].Equals(row["Цена", DataRowVersion.Current]);
//        }
//    }
//}

//3
//using System;
//using System.Data;
//using System.Linq;

//namespace AdoNetDataViewDemo
//{
//    class Program
//    {
//        static DataSet ds = new DataSet("CompanyDS");

//        static void Main(string[] args)
//        {
//            Console.OutputEncoding = System.Text.Encoding.UTF8;

//            CreateTables();
//            FillSampleData();

//            Console.WriteLine("СИСТЕМА УПРАВЛЕНИЯ СОТРУДНИКАМИ И ПРОЕКТАМИ\n");
//            Console.WriteLine("Исходные данные:");
//            PrintTable(ds.Tables["Сотрудники"]);
//            PrintTable(ds.Tables["Проекты"]);

//            Console.WriteLine("\n1. Поиск сотрудников по фамилии 'Ивано'");
//            FindEmployeesByLastName("Ивано");

//            Console.WriteLine("\n2. Сотрудники IT-отдела (через DataView.RowFilter)");
//            FilterByDepartmentWithDataView("IT");

//            Console.WriteLine("\n3. Сотрудники с зарплатой > 100000");
//            FilterBySalary(100000);

//            Console.WriteLine("\n4. Сотрудники по дате найма (по убыванию)");
//            SortByHireDate descending: true);

//            Console.WriteLine("\n5. IT-сотрудники с зарплатой > 80000, отсортированные по ФИО");
//            CombinedFilterAndSort();

//            Console.WriteLine("\n6. Статистика по сотрудникам с зарплатой > 70000:");
//            ShowStatistics(salaryThreshold: 70000);

//            Console.WriteLine("\nГотово! Нажмите любую клавишу...");
//            Console.ReadKey();
//        }

//        static void CreateTables()
//        {
//            DataTable emp = new DataTable("Сотрудники");
//            emp.Columns.Add("ID", typeof(int)).AutoIncrement = true;
//            emp.Columns["ID"].AutoIncrementSeed = 1;
//            emp.Columns.Add("ФИО", typeof(string));
//            emp.Columns.Add("Отдел", typeof(string));
//            emp.Columns.Add("Зарплата", typeof(decimal));
//            emp.Columns.Add("ДатаНайма", typeof(DateTime));
//            emp.PrimaryKey = new DataColumn[] { emp.Columns["ID"] };
//            ds.Tables.Add(emp);

//            DataTable proj = new DataTable("Проекты");
//            proj.Columns.Add("ID", typeof(int)).AutoIncrement = true;
//            proj.Columns["ID"].AutoIncrementSeed = 1;
//            proj.Columns.Add("Название", typeof(string));
//            proj.Columns.Add("Отдел", typeof(string));
//            proj.Columns.Add("БюджетПроекта", typeof(decimal));
//            proj.Columns.Add("ДатаНачала", typeof(DateTime));
//            proj.PrimaryKey = new DataColumn[] { proj.Columns["ID"] };
//            ds.Tables.Add(proj);
//        }

//        static void FillSampleData()
//        {
//            var employees = ds.Tables["Сотрудники"];
//            employees.Rows.Add(null, "Иванов Иван Петрович", "IT", 120000m, new DateTime(2020, 5, 15));
//            employees.Rows.Add(null, "Петрова Анна Сергеевна", "Бухгалтерия", 85000m, new DateTime(2019, 3, 22));
//            employees.Rows.Add(null, "Сидоров Алексей Дмитриевич", "IT", 135000m, new DateTime(2021, 8, 10));
//            employees.Rows.Add(null, "Козлова Мария Владимировна", "HR", 75000m, new DateTime(2022, 1, 5));
//            employees.Rows.Add(null, "Новиков Сергей Александрович", "IT", 95000m, new DateTime(2020, 11, 30));
//            employees.Rows.Add(null, "Морозова Екатерина Ильинична", "Маркетинг", 90000m, new DateTime(2023, 2, 14));

//            var projects = ds.Tables["Проекты"];
//            projects.Rows.Add(null, "CRM Система", "IT", 5000000m, new DateTime(2024, 1, 10));
//            projects.Rows.Add(null, "Ребрендинг", "Маркетинг", 1200000m, new DateTime(2024, 6, 1));
//            projects.Rows.Add(null, "Автоматизация склада", "IT", 8000000m, new DateTime(2023, 9, 15));

//            employees.AcceptChanges();
//            projects.AcceptChanges();
//        }

//        static void FindEmployeesByLastName(string partialName)
//        {
//            try
//            {
//                DataRow[] found = ds.Tables["Сотрудники"].Select($"ФИО LIKE '%{partialName}%'");
//                PrintRows(found, $"Найдено сотрудников по запросу '{partialName}': {found.Length}");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Ошибка фильтрации: {ex.Message}");
//            }
//        }

//        static void FilterByDepartmentWithDataView(string department)
//        {
//            DataView dv = new DataView(ds.Tables["Сотрудники"])
//            {
//                RowFilter = $"Отдел = '{department}'",
//                Sort = "Зарплата DESC"
//            };

//            PrintDataView(dv, $"Сотрудники отдела '{department}' (отсортированы по убыванию зарплаты)");
//        }

//        static void FilterBySalary(decimal minSalary)
//        {
//            DataView dv = new DataView(ds.Tables["Сотрудники"])
//            {
//                RowFilter = $"Зарплата > {minSalary}"
//            };
//            PrintDataView(dv, $"Сотрудники с зарплатой > {minSalary:N0}");
//        }

//        static void SortByHireDate(bool descending = false)
//        {
//            DataView dv = new DataView(ds.Tables["Сотрудники"])
//            {
//                Sort = descending ? "ДатаНайма DESC" : "ДатаНайма ASC"
//            };
//            string order = descending ? "убыванию" : "возрастанию";
//            PrintDataView(dv, $"Сотрудники по дате найма (по {order})");
//        }

//        static void CombinedFilterAndSort()
//        {
//            DataView dv = new DataView(ds.Tables["Сотрудники"])
//            {
//                RowFilter = "Отдел = 'IT' AND Зарплата > 80000",
//                Sort = "ФИО ASC"
//            };

//            PrintDataView(dv, "IT-специалисты с зарплатой > 80000, по алфавиту");
//        }

//        static void ShowStatistics(decimal? salaryThreshold = null)
//        {
//            string filter = salaryThreshold.HasValue ? $"Зарплата > {salaryThreshold}" : null;
//            DataRow[] rows = filter != null
//                ? ds.Tables["Сотрудники"].Select(filter)
//                : ds.Tables["Сотрудники"].Select();

//            if (rows.Length == 0)
//            {
//                Console.WriteLine("Нет данных для статистики.");
//                return;
//            }

//            decimal avgSalary = rows.Average(r => Convert.ToDecimal(r["Зарплата"]));
//            decimal maxSalary = rows.Max(r => Convert.ToDecimal(r["Зарплата"]));
//            decimal minSalary = rows.Min(r => Convert.ToDecimal(r["Зарплата"]));
//            var deptCount = rows.GroupBy(r => r["Отдел"])
//                                .Select(g => new { Отдел = g.Key, Количество = g.Count() });

//            Console.WriteLine($"Количество сотрудников: {rows.Length}");
//            Console.WriteLine($"Средняя зарплата: {avgSalary:N2}");
//            Console.WriteLine($"Максимальная зарплата: {maxSalary:N0}");
//            Console.WriteLine($"Минимальная зарплата: {minSalary:N0}");
//            Console.WriteLine("По отделам:");
//            foreach (var d in deptCount)
//                Console.WriteLine($"  {d.Отдел}: {d.Количество} чел.");
//        }

//        static void PrintTable(DataTable table)
//        {
//            Console.WriteLine($"\n--- {table.TableName} ---");
//            Console.WriteLine(string.Join(" | ", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName.PadRight(20))));
//            Console.WriteLine(new string('-', table.Columns.Count * 22));

//            foreach (DataRow row in table.Rows)
//            {
//                Console.WriteLine(string.Join(" | ", row.ItemArray.Select(x => x.ToString().PadRight(20))));
//            }
//            Console.WriteLine();
//        }

//        static void PrintRows(DataRow[] rows, string title)
//        {
//            Console.WriteLine($"\n{title}");
//            if (rows.Length == 0)
//            {
//                Console.WriteLine("  Ничего не найдено.");
//                return;
//            }
//            Console.WriteLine("  ID | ФИО                     | Отдел       | Зарплата   | Дата найма");
//            Console.WriteLine("  " + new string('-', 80));
//            foreach (DataRow r in rows)
//            {
//                Console.WriteLine($"  {r["ID"],2} | {r["ФИО"],-23} | {r["Отдел"],-11} | {r["Зарплата"],10:N0} | {((DateTime)r["ДатаНайма"]):yyyy-MM-dd}");
//            }
//        }

//        static void PrintDataView(DataView dv, string title)
//        {
//            Console.WriteLine($"\n{title} [{dv.Count} записей]");
//            Console.WriteLine("  ID | ФИО                     | Отдел       | Зарплата   | Дата найма");
//            Console.WriteLine("  " + new string('-', 80));
//            foreach (DataRowView drv in dv)
//            {
//                DataRow r = drv.Row;
//                Console.WriteLine($"  {r["ID"],2} | {r["ФИО"],-23} | {r["Отдел"],-11} | {r["Зарплата"],10:N0} | {((DateTime)r["ДатаНайма"]):yyyy-MM-dd}");
//            }
//        }
//    }
//}