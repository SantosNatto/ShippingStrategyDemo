using System;
using System.Collections.Generic;
using System.Globalization;

namespace ShippingStrategyDemo
{
    // Interface da estratégia
    public interface IShippingStrategy
    {
        decimal Calculate(decimal weightKg, decimal distanceKm, decimal priceBeforeShipping);
        string Name { get; }
    }

    // Estratégia: Sedex (mais rápida, mais cara)
    public class SedexStrategy : IShippingStrategy
    {
        public string Name => "Sedex (Rápido)";

        public decimal Calculate(decimal weightKg, decimal distanceKm, decimal priceBeforeShipping)
        {
            // Exemplo simples: taxa base + por kg + por km + percentual sobre o preço do produto
            decimal baseRate = 12.00m;
            decimal perKg = 4.50m * weightKg;
            decimal perKm = 0.10m * distanceKm;
            decimal percentOnPrice = priceBeforeShipping * 0.03m; // 3% do valor do pedido

            return Decimal.Round(baseRate + perKg + perKm + percentOnPrice, 2);
        }
    }

    // Estratégia: PAC (mais barato, mais lento)
    public class PacStrategy : IShippingStrategy
    {
        public string Name => "PAC (Econômico)";

        public decimal Calculate(decimal weightKg, decimal distanceKm, decimal priceBeforeShipping)
        {
            decimal baseRate = 6.00m;
            decimal perKg = 2.80m * weightKg;
            decimal perKm = 0.06m * distanceKm;
            decimal percentOnPrice = priceBeforeShipping * 0.01m; // 1% do valor do pedido

            return Decimal.Round(baseRate + perKg + perKm + percentOnPrice, 2);
        }
    }

    // Estratégia: Retirada no local (grátis)
    public class PickupStrategy : IShippingStrategy
    {
        public string Name => "Retirada no Local (Grátis)";

        public decimal Calculate(decimal weightKg, decimal distanceKm, decimal priceBeforeShipping)
        {
            return 0.00m;
        }
    }

    // Estratégia: Promoção (frete grátis acima de X)
    public class PromotionalFreeShippingStrategy : IShippingStrategy
    {
        public string Name => "Promoção: Frete grátis acima de valor";

        private readonly decimal threshold;

        public PromotionalFreeShippingStrategy(decimal threshold)
        {
            this.threshold = threshold;
        }

        public decimal Calculate(decimal weightKg, decimal distanceKm, decimal priceBeforeShipping)
        {
            if (priceBeforeShipping >= threshold) return 0m;

            // Se abaixo do limite, aplicar PAC como fallback
            var pac = new PacStrategy();
            return pac.Calculate(weightKg, distanceKm, priceBeforeShipping);
        }
    }

    // Contexto: Pedido
    public class Order
    {
        public string Id { get; }
        public decimal PriceBeforeShipping { get; }
        public decimal WeightKg { get; }
        public decimal DistanceKm { get; }
        private IShippingStrategy shippingStrategy;

        public Order(string id, decimal priceBeforeShipping, decimal weightKg, decimal distanceKm, IShippingStrategy initialStrategy)
        {
            Id = id;
            PriceBeforeShipping = priceBeforeShipping;
            WeightKg = weightKg;
            DistanceKm = distanceKm;
            shippingStrategy = initialStrategy ?? throw new ArgumentNullException(nameof(initialStrategy));
        }

        public void SetShippingStrategy(IShippingStrategy strategy)
        {
            shippingStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public decimal CalculateShipping()
        {
            return shippingStrategy.Calculate(WeightKg, DistanceKm, PriceBeforeShipping);
        }

        public decimal Total()
        {
            return Decimal.Round(PriceBeforeShipping + CalculateShipping(), 2);
        }

        public string ShippingMethodName => shippingStrategy.Name;
    }

    class Program
    {
        static void Main()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

            // Criando estratégias disponíveis
            var sedex = new SedexStrategy();
            var pac = new PacStrategy();
            var pickup = new PickupStrategy();
            var promo = new PromotionalFreeShippingStrategy(threshold: 300m); // frete grátis acima de R$300

            // Lista de pedidos exemplo
            var orders = new List<Order>
            {
                new Order("PED001", priceBeforeShipping: 120.50m, weightKg: 2.2m, distanceKm: 150m, initialStrategy: pac),
                new Order("PED002", priceBeforeShipping: 35.00m, weightKg: 0.5m, distanceKm: 12m, initialStrategy: sedex),
                new Order("PED003", priceBeforeShipping: 420.00m, weightKg: 5.0m, distanceKm: 500m, initialStrategy: promo),
                new Order("PED004", priceBeforeShipping: 80.00m, weightKg: 1.0m, distanceKm: 0m, initialStrategy: pickup),
            };

            Console.WriteLine("=== Sistema de Cálculo de Frete (Pattern: Strategy) ===\n");

            foreach (var order in orders)
            {
                PrintOrderSummary(order);
            }

            // Demonstração: trocar estratégia em tempo de execução
            var dynamicOrder = new Order("PED100", 250m, 3.0m, 320m, pac);
            Console.WriteLine("\n--- Pedido com troca de estratégia em tempo de execução ---");
            PrintOrderSummary(dynamicOrder);

            Console.WriteLine("\nAplicando Sedex...");
            dynamicOrder.SetShippingStrategy(sedex);
            PrintOrderSummary(dynamicOrder);

            Console.WriteLine("\nAplicando Promoção (frete grátis se >= R$300)...");
            dynamicOrder.SetShippingStrategy(promo);
            PrintOrderSummary(dynamicOrder);

            Console.WriteLine("\nFim. Pressione Enter para sair.");
            Console.ReadLine();
        }

        static void PrintOrderSummary(Order order)
        {
            Console.WriteLine($"Pedido: {order.Id}");
            Console.WriteLine($" - Método de frete: {order.ShippingMethodName}");
            Console.WriteLine($" - Valor dos produtos: {order.PriceBeforeShipping:C}");
            Console.WriteLine($" - Peso: {order.WeightKg} kg");
            Console.WriteLine($" - Distância: {order.DistanceKm} km");
            Console.WriteLine($" - Valor do frete: {order.CalculateShipping():C}");
            Console.WriteLine($" - Total (produto + frete): {order.Total():C}\n");
        }
    }
}
