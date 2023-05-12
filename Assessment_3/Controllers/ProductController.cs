namespace Assessment_3.Controllers
{
    using Assessment_3.Interfaces;
    using Assessment_3.Models;

    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using FluentValidation;
    using FluentValidation.AspNetCore;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    public class ProductController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IValidator<Product> productValidator;

        public ProductController(IProductsService productsService, IValidator<Product> productValidator)
        {
            this.productsService = productsService;
            this.productValidator = productValidator;
        }

        public IActionResult Index(int categoryId)
        {
            this.ViewBag.CategoryId = categoryId;
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            int categoryId = 0;
            var queryParams = this.Request.Query.ToDictionary(_ => _.Key, _ => _.Value);
            if (queryParams.ContainsKey("categoryId"))
            {
                categoryId = int.Parse(queryParams["categoryId"]);
            }

            return DataSourceLoader.Load(this.productsService.FindAllByField("CategoryId", categoryId), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = JsonConvert.DeserializeObject<Product>(values);

            if (model == null)
            {
                this.ModelState.AddModelError("all", "1 one or more properties malformed");
                return this.BadRequest(this.ModelState);
            }

            var result = this.productValidator.Validate(model, _ => _.IncludeRuleSets("Create"));
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.productsService.Add(model);

            return this.Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var model = new Product() { ProductId = key };

            var result = this.productValidator.Validate(model, _ => _.IncludeRuleSets("Delete"));
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.productsService.Delete(key);

            return this.Ok();
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var model = this.productsService.GetById(key);

            JsonConvert.PopulateObject(values, model);

            var result = this.productValidator.Validate(model, _ => _.IncludeRuleSets("Edit"));
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.productsService.Update(model);

            return this.Ok();
        }
    }
}
