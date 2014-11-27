using System;
using System.Collections.Generic;

namespace HoaxFramework
{
	public class MethodTimeTracker
	{
		private static volatile MethodTimeTracker instance;
		private static object syncRoot = new Object();

		private MethodTimeTracker() {}

		public static MethodTimeTracker Instance
		{
			get 
			{
				if (instance == null) 
				{
					lock (syncRoot) 
					{
						if (instance == null) 
							instance = new MethodTimeTracker();
					}
				}

				return instance;
			}
		}

		private Dictionary<String, long> MethodTimes = new Dictionary<String, long>();
		private Dictionary<String, long> MethodCallCount = new Dictionary<String, long>();
		private Dictionary<String, long> CurrentMethodTimes = new Dictionary<String, long>();
		private long start;

		public void Start(string name)
		{
			if (CurrentMethodTimes.ContainsKey(name)) {
				Stop (name);
			} else CurrentMethodTimes [name] = CurrentTimeMillis ();

			if (!MethodCallCount.ContainsKey (name))
				MethodCallCount [name] = 0;
			MethodCallCount [name]++;
		}

		public void Stop(string name)
		{
			if (CurrentMethodTimes.ContainsKey (name)) {
				if (!MethodTimes.ContainsKey (name))
					MethodTimes [name] = 0;

				MethodTimes [name] += CurrentTimeMillis () - CurrentMethodTimes [name];
				CurrentMethodTimes.Remove (name);
			}
		}

		private static readonly DateTime Jan1st1970 = new DateTime
			(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static long CurrentTimeMillis()
		{
			return (long) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
		}

		public string ToString() 
		{
			long sum = 0;
			foreach (var k in MethodTimes.Keys)
				sum += MethodTimes [k];

			string s ="Method name".PadRight (150, ' ') + "\t" + "Calls" +"\t" + "Time [ms]".PadRight (20, ' ') + "\t" + "Time [%]" + "\t\t" + "Ratio" + " \n" + "".PadRight(230, '-') + "\n";
			foreach (var k in MethodTimes.Keys) {
				float relativeTime = ((MethodTimes [k] * 100) / (float)sum);
				decimal percent = Math.Round ((Decimal)relativeTime, 3, MidpointRounding.AwayFromZero);
				decimal ratio =  Math.Round (1-(Decimal)(MethodCallCount[k]/(relativeTime+0.00001)), 3, MidpointRounding.AwayFromZero);
				s += k.PadRight (150, ' ') + "\t" + MethodCallCount[k] + "\t" +MethodTimes [k].ToString ().PadRight (20, ' ') + "\t" + percent + "%\t\t" + ratio + " \n";
			}
			return s;
		}
	}
}

