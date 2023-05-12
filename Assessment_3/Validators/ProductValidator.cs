namespace Assessment_3.Validators
{
    using Assessment_3.Models;

    using FluentValidation;

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            this.RuleSet("Create", () =>
            {
                this.GeneralRules();
            });

            this.RuleSet("Edit", () =>
            {
                this.GeneralRules();

                this.RuleFor(_ => _.ProductId)
                    .NotEmpty()
                    .GreaterThan(0);
            });

            this.RuleSet("Delete", () =>
            {
                this.RuleFor(_ => _.ProductId)
                    .NotEmpty()
                    .GreaterThan(0);
            });
        }

        private void GeneralRules()
        {
            this.RuleFor(_ => _.Name)
                .NotEmpty()
                .MaximumLength(50);

            this.RuleFor(_ => _.Description)
                .NotEmpty();

            this.RuleFor(_ => _.CategoryId)
                .GreaterThan(0);

            this.RuleFor(_ => _.Price)
                .NotEmpty()
                .GreaterThan(0);

            this.RuleFor(_ => _.Quantity)
                .NotNull();
        }
    }
}
