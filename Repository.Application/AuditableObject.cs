using Interface.Application;

namespace Repository.Application.DataModel
{
    class AuditableObject
    {
    }
    public partial class ARMADA : IAuditableObject
    {
    }
    public partial class EMPLOYEE : IAuditableObject
    {
    }
    public partial class PRODUCT : IAuditableObject
    {
    }
    public partial class RITASE : IAuditableObject
    {
    }
    public partial class SITE : IAuditableObject
    {
    }
    public partial class SOURCE : IAuditableObject
    {
    }
    public partial class CLIENT : IAuditableObject
    {
    }
    public partial class BATCH : IAuditableObject
    {
    }
    public partial class CONTRACT : IAuditableObject
    {
    }
    public partial class BatchProduct : IAuditableObject
    {
    }
    public partial class StockProduct : IAuditableObject
    {
    }
    public partial class TransactionProduct : IAuditableObject
    {

    }
    public partial class ProductSite : IAuditableObject
    {

    }
    public partial class ProductAdjustment : IAuditableObject
    {

    }
    public partial class ProductConvertion : IAuditableObject
    {

    }
    public partial class DeliveryRequest : IAuditableObject
    {
    }
    public partial class UserClientMapping : IAuditableObject
    {

    }
    public partial class ContractProduct : IAuditableObject
    {
    }
    public partial class DeliveryRequestProductDetailTransaction : IAuditableObject
    {
    }
    public partial class FinanceTransaction : IAuditableObject
    {
    }
    public partial class FinanceBalance : IAuditableObject
    {
    }
    public partial class NostroBank : IAuditableObject
    {
    }
    public partial class TransactionCode : IAuditableObject
    {
    }
    public partial class FinanceTransactionNostro : IAuditableObject
    {
    }

    public partial class AspNetGroups : IAuditableObject
    {
    }

    public partial class AspNetGroupUser : IAuditableObject
    {
    }

    public partial class AspNetRoleGroup : IAuditableObject
    {
    }

    public partial class AspNetRoles : IAuditableObject
    {
    }
    public partial class AspNetUsers : IAuditableObject
    {
    }
    public partial class Menu : IAuditableObject
    {
    }
    public partial class DeliveryOrder  : IAuditableObject
    {
    }
    public partial class DeliveryOrderInvoice  : IAuditableObject
    {
        public virtual DeliveryOrder DeliveryOrder { get; set; }

    }


}