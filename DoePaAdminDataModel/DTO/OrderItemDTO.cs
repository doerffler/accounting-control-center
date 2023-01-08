namespace DoePaAdminDataModel.DTO;

public class OrderItemDTO
{
    public int OrderItemPosition { get; set; }
    public string ItemBillingUnitCode { get; set; }
    public decimal OrderVolumeAmount { get; set; }
    public string OrderItemDescription { get; set; }
    public string ItemCurrencyISO { get; set; }
}