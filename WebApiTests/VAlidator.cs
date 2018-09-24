using FluentValidation;
using WebApiTests.Models;
using WebApiTests.Services;

namespace WebApiTests
{
    public class VAlidator : AbstractValidator<PurchaseOrderType>
    {
        private readonly ISerialiseMessage<PurchaseOrderType> _serialiseMessage;

        public VAlidator(ISerialiseMessage<PurchaseOrderType> serialiseMessage)
        {
            _serialiseMessage = serialiseMessage;
            RuleFor(type => type.confirmDate).NotNull().NotEmpty();
        }
    }
}