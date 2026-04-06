using ITHSystems.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.DTOs;

public sealed record class ProductDto
{
    public int Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Tax { get; set; } = new();
    public decimal Cost { get; set; } = new();
    public decimal Price { get; set; } = new();
    public int ImageId { get; set; } = new();
    public string ImageName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public ImageSource ImageSource => UtilExtensions.ConvertBase64ToImageSource(Image);


}
