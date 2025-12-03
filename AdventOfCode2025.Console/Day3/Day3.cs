using System.Text;

namespace AdventOfCode2025.Console.Day3;

public static class Day3
{
    public static long Solve()
    {
        var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day3", "day3.txt"));

        var banks = input.Split(Environment.NewLine);

        long totalOutput = 0;
        foreach (var bank in banks)
        {
            var bankOutput = GetBankOutput2(bank, 12);

            totalOutput += bankOutput;
        }

        return totalOutput;
    }

    private static int GetBankOutput1(string bank)
    {
        var bankOutput = 0;

        for (var i = 0; i < bank.Length - 1; i++)
        {
            for (var j = (i + 1); j < bank.Length; j++)
            {
                var outputString = $"{bank[i]}{bank[j]}";

                var outputValue = int.Parse(outputString);
                if (outputValue > bankOutput)
                {
                    bankOutput = outputValue;
                }
            }
        }

        return bankOutput;
    }

    /// <summary>
    /// Find the largest output for a given bank
    /// </summary>
    /// <param name="bank">The bank input as a string</param>
    /// <param name="batteries">The number of batteries in the bank to switch on</param>
    /// <returns></returns>
    private static long GetBankOutput2(string bank, int batteries)
    {
        var indexes = new List<int>(); // Store the indexes of the batteries to switch on at the end

        var startingIndex = 0;

        for (var i = 0; i < batteries; i++)
        {
            // We need to find the largest number in the bank
            var maxLength = bank.Length - (batteries - i);

            var maxIndex = GetMaxIndex(bank, maxLength, startingIndex);
            indexes.Add(maxIndex);

            startingIndex = maxIndex + 1;
        }

        var constructedBattery = new StringBuilder();
        indexes.ForEach(x => constructedBattery.Append(bank[x]));

        return long.Parse(constructedBattery.ToString());
    }

    private static int GetMaxIndex(string bank, int maxLength, int startingIndex)
    {
        var maxIndex = 0;
        var maxValue = 0;

        for (var i = startingIndex; i <= maxLength; i++)
        {
            var currentValue = int.Parse(bank[i].ToString());
            if (currentValue > maxValue)
            {
                maxValue = currentValue;
                maxIndex = i;
            }
        }

        return maxIndex;
    }
}
