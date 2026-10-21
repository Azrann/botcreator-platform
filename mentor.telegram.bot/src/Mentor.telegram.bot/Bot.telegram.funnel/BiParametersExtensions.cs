using System;
using System.Collections.Generic;

namespace Bot.telegram.funnel
{
    public static class BiParametersExtensions
    {
        private const string OrderType = "OrderType";
        private const string Pair = "Pair";
        private const string PairLeft = "PairLeft";
        private const string PairRight = "PairRight";
        private const string OrderSide = "OrderSide";
        private const string OrderAmount = "OrderAmount";
        private const string LimitPrice = "LimitPrice";
        private const string StopLimitPrice = "StopLimitPrice";
        private const string CurrentPrice = "CurrentPrice";
        private const string BiTotal = "BiTotal";

        public static string GetOrderType(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(OrderType))
            {
                return (string)parameters[OrderType];
            }

            throw new ArgumentException($"OrderType not found in parameters");
        }

        public static void AddOrderType(this Dictionary<string, object> parameters, string orderType)
        {
            if (parameters.ContainsKey(OrderType))
            {
                parameters[OrderType] = orderType;
                return;
            }
            parameters.Add(OrderType, orderType);
        }

        public static string GetPair(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(Pair))
            {
                return (string)parameters[Pair];
            }

            return null;
        }

        public static void AddPair(this Dictionary<string, object> parameters, string pair)
        {
            if (parameters.ContainsKey(Pair))
            {
                parameters[Pair] = pair;
                return;
            }
            parameters.Add(Pair, pair);
        }

        public static string GetPairRight(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(PairRight))
            {
                return (string)parameters[PairRight];
            }

            return null;
        }

        public static void AddPairRight(this Dictionary<string, object> parameters, string pairRight)
        {
            if (parameters.ContainsKey(PairRight))
            {
                parameters[PairRight] = pairRight;
                return;
            }
            parameters.Add(PairRight, pairRight);
        }

        public static string GetPairLeft(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(PairLeft))
            {
                return (string)parameters[PairLeft];
            }

            return null;
        }

        public static void AddPairLeft(this Dictionary<string, object> parameters, string pairLeft)
        {
            if (parameters.ContainsKey(PairLeft))
            {
                parameters[PairLeft] = pairLeft;
                return;
            }
            parameters.Add(PairLeft, pairLeft);
        }

        public static string GetOrderSide(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(OrderSide))
            {
                return (string)parameters[OrderSide];
            }

            return null;
        }

        public static void AddOrderSide(this Dictionary<string, object> parameters, string orderSide)
        {
            if (parameters.ContainsKey(OrderSide))
            {
                parameters[OrderSide] = orderSide;
                return;
            }
            parameters.Add(OrderSide, orderSide);
        }

        public static decimal GetOrderAmount(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(OrderAmount))
            {
                if (parameters[OrderAmount] is string orderAmountString)
                {
                    return decimal.Parse(orderAmountString);
                }
                return (decimal)parameters[OrderAmount];
            }

            throw new ArgumentException($"OrderAmount not found in parameters");
        }

        public static void AddOrderAmount(this Dictionary<string, object> parameters, decimal orderAmount)
        {
            if (parameters.ContainsKey(OrderAmount))
            {
                parameters[OrderAmount] = orderAmount;
                return;
            }
            parameters.Add(OrderAmount, orderAmount);
        }

        public static decimal GetCurrentPrice(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(CurrentPrice))
            {
                return (decimal)parameters[CurrentPrice];
            }

            throw new ArgumentException($"CurrentPrice not found in parameters");
        }

        public static void AddCurrentPrice(this Dictionary<string, object> parameters, decimal currentPrice)
        {
            if (parameters.ContainsKey(CurrentPrice))
            {
                parameters[CurrentPrice] = currentPrice;
                return;
            }
            parameters.Add(CurrentPrice, currentPrice);
        }
        public static decimal GetLimitPrice(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(LimitPrice))
            {
                if (parameters[LimitPrice] is string limitPriceString)
                {
                    return decimal.Parse(limitPriceString);
                }

                return (decimal)parameters[LimitPrice];
            }

            throw new ArgumentException($"LimitPrice not found in parameters");
        }

        public static void AddLimitPrice(this Dictionary<string, object> parameters, decimal limitPrice)
        {
            if (parameters.ContainsKey(LimitPrice))
            {
                parameters[LimitPrice] = limitPrice;
                return;
            }
            parameters.Add(LimitPrice, limitPrice);
        }

        public static string GetBiTotal(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(BiTotal))
            {
                return (string)parameters[BiTotal];
            }

            throw new ArgumentException($"BiTotal not found in parameters");
        }

        public static void AddBiTotal(this Dictionary<string, object> parameters, string biTotal)
        {
            if (parameters.ContainsKey(BiTotal))
            {
                parameters[BiTotal] = biTotal;
                return;
            }
            parameters.Add(BiTotal, biTotal);
        }

        public static decimal GetStopLimitPrice(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(StopLimitPrice))
            {
                if (parameters[StopLimitPrice] is string stopLimitPriceString)
                {
                    return decimal.Parse(stopLimitPriceString);
                }

                return (decimal)parameters[StopLimitPrice];
            }

            throw new ArgumentException($"StopLimitPrice not found in parameters");
        }

        public static void AddStopLimitPrice(this Dictionary<string, object> parameters, decimal stopLimitPrice)
        {
            if (parameters.ContainsKey(StopLimitPrice))
            {
                parameters[StopLimitPrice] = stopLimitPrice;
                return;
            }
            parameters.Add(StopLimitPrice, stopLimitPrice);
        }
    }
}