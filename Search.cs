using System;
using System.Collections.Generic;
public class ItemSummary
{
    public string itemId { get; set; }
    public string title { get; set; }
    public Image image { get; set; }
    public Price price { get; set; }
    public string itemHref { get; set; }
    public Seller seller { get; set; }
    public string condition { get; set; }
    public string conditionId { get; set; }
    public List<ThumbnailImage> thumbnailImages { get; set; }
    public List<ShippingOption> shippingOptions { get; set; }
    public List<string> buyingOptions { get; set; }
    public CurrentBidPrice currentBidPrice { get; set; }
    public string itemAffiliateWebUrl { get; set; }
    public string itemWebUrl { get; set; }
    public ItemLocation itemLocation { get; set; }
    public List<Category> categories { get; set; }
    public bool adultOnly { get; set; }
}
public class Price
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class Image
{
    public string imageUrl { get; set; }
}

public class Seller
{
    public string username { get; set; }
    public string feedbackPercentage { get; set; }
    public int feedbackScore { get; set; }
}

public class ThumbnailImage
{
    public string imageUrl { get; set; }
}

public class ShippingCost
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class ShippingOption
{
    public string shippingCostType { get; set; }
    public ShippingCost shippingCost { get; set; }
}

public class CurrentBidPrice
{
    public string value { get; set; }
    public string currency { get; set; }
}

public class ItemLocation
{
    public string postalCode { get; set; }
    public string country { get; set; }
}

public class Category
{
    public string categoryId { get; set; }
}
public class RootObject
{
    public string href { get; set; }
    public int total { get; set; }
    public string next { get; set; }
    public int limit { get; set; }
    public int offset { get; set; }
    public List<ItemSummary> itemSummaries { get; set; }
}