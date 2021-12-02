using EventTicket.Services.Payment.Model;
using System.Threading.Tasks;

namespace EventTicket.Services.Payment.Services
{
    public interface IExternalGatewayPaymentService
    {
        Task<bool> PerformPayment(PaymentInfo paymentInfo);
    }
}
