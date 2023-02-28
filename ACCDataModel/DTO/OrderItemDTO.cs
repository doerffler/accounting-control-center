namespace ACCDataModel.DTO;

public class OrderItemDTO
{
    public int OrderItemPosition { get; set; }
    public string ItemBillingUnitCode { get; set; }
    public decimal OrderVolumeAmount { get; set; }
    public string OrderItemDescription { get; set; }
    public decimal NetUnitPrice { get; set; }
}