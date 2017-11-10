//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//namespace FrontEndApp.Controllers
//{
//    public class ProductInfoController : ViewComponent
//    {
//        //Instead of a context this can be and will be a call to the api
//        private readonly ProductContext db;

//        public ProductInfoController(ProductContext context)
//        {
//            db = context;
//        }

//        public async Task<IViewComponentResult> InvokeAsync(long sku)
//        {
//            //again instead of context this will be a call on the information retrieved from the api call 
//            var items = await db.stock
//                .where(x => x.Sku == sku)
//                .ToListAsync();
//            return View(items);
//        }
//    }
//}