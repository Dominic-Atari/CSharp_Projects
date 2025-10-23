
namespace Dominic.Net.Models
{
    public class ShoppingCart : IShoppingCart
    {

        private readonly DominicShopDbContext _context;
        // Each shopping cart is identified by a unique ID, typically stored in session.
        public string ShoppingCartId { get; set; } = string.Empty;

        public ShoppingCart(DominicShopDbContext context)
        {
            _context = context;
        }
        public List<ShoppingCartItem> ShoppingCartItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // main purpose of this method is to get the cart for the current user session. It checks if a cart ID exists in the session; if not, it creates a new one.
        public static ShoppingCart GetCart(IServiceProvider serviceProvider)
        {
            ISession? session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            DominicShopDbContext context = serviceProvider.GetService<DominicShopDbContext>() ?? throw new Exception("Error initializing shopping cart");

            string? cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Pie pie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            throw new NotImplementedException();
        }

        public decimal GetShoppingCartTotal()
        {
            throw new NotImplementedException();
        }

        public int RemoveFromCart(Pie pie)
        {
            throw new NotImplementedException();
        }
    }
}