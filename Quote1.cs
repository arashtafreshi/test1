using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4
{
    public class Quote1
    {
        public int Id { get; set; }
        public String symbol { get; set; }
        public String name { get; set; }
        public String dayCode { get; set; }
        public DateTime serverTimestamp { get; set; }
        public String mode { get; set; }
        public float lastPrice { get; set; }
        public int tradeSize { get; set; }
        public float netChange { get; set; }
        public float percentChange { get; set; }
        public String tick { get; set; }
        public float previousLastPrice { get; set; }
        public DateTime previousTimestamp { get; set; }
        public float bid { get; set; }
        public int bidSize { get; set; }
        public float ask { get; set; }
        public int askSize { get; set; }
        public String unitCode { get; set; }
        public float open { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float close { get; set; }
        public int numTrades { get; set; }
        public float dollarVolume { get; set; }
        public String flag { get; set; }
        public float previousClose { get; set; }
        public float settlement { get; set; }
        public float previousSettlement { get; set; }
        public int volume { get; set; }
        public int previousVolume { get; set; }
        public float openInterest { get; set; }
        public float fiftyTwoWkHigh { get; set; }
        public DateTime fiftyTwoWkHighDateTime { get; set; }
        public float fiftyTwoWkLow { get; set; }
        public DateTime fiftyTwoWkLowDateTime { get; set; }
        public int avgVolume { get; set; }
        public int sharesOutstanding { get; set; }
        public float dividendRateAnnual { get; set; }
        public float dividendYieldAnnual { get; set; }
        public DateTime exDividendDateTime { get; set; }
        public float impliedVolatility { get; set; }
        public float twentyDayAvgVol { get; set; }
        public String month { get; set; }
        public String year { get; set; }
        public DateTime expirationDateTime { get; set; }
        public String lastTradingDay { get; set; }
        public float twelveMnthPct { get; set; }
        public DateTime twelveMnthPctDateTime { get; set; }
        public float preMarketPrice { get; set; }
        public float preMarketNetChange { get; set; }
        public float preMarketPercentChange { get; set; }
        public DateTime preMarketTimestamp { get; set; }
        public float afterHoursPrice { get; set; }
        public float afterHoursNetChange { get; set; }
        public float afterHoursPercentChange { get; set; }
        public DateTime afterHoursTimestamp { get; set; }
        public int averageWeeklyVolume { get; set; }
        public int averageMonthlyVolume { get; set; }
        public int averageQuarterlyVolume { get; set; }
        public String exchangeMargin { get; set; }
        
    }
}