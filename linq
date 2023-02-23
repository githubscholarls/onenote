join into 相当于按照on条件分了组

var groupJoinQuery2 =
            from category in categories
            orderby category.ID
            join prod in products on category.ID equals prod.CategoryID into prodGroup
            select new
            {
                Category = category.Name,
                Products = from prod2 in prodGroup
                           orderby prod2.Name
                           select prod2
            };


--不能select 一个分组
(group c by c.name into  ac  select ac).ToList();
