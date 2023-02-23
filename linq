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

--客户端分析错误

--不能select 一个分组
(group c by c.name into  ac  select ac).ToList();
--改正
(group c by c.name into  ac  select new { key = ac.Key, value = ac.ToList()}).ToList();
--不能linq查询中使用内存中数据，要写成子查询linq
var ins =new List<orderTypes>();
group o by o.UserId into oot
                                     select new
                                     {
                                         employeeId = oot.Key,
                                         employeeName = (from u in tempCrmDbContext.WtcrmSysUserInfos where u.UserId==oot.Key select u.UserName).FirstOrDefault()??"",
                                         empOrders = oot.GroupBy(s => s.OrderType).Select(g => new
                                         {
                                             productName = ins.where(o=>o.OrderTypeId==g.Key).Select(o=>o.Name).FirstOrDefalut()??"",  //(from o in tempCrmDbContext.WtcrmSysOrderTypes where o.OrderTypeId == g.Key select o.OrderTypeName).FirstOrDefault()??"",
                                             productNum = g.Count(),
                                             Amount = g.Sum(o => o.OrderAmount)
                                         })
                                     }).ToList();
