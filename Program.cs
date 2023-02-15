
internal class Program
{   
    static int[] snafuDigitDictionary;
    static char[] snafuChar = {'=','-','0','1','2'};
    private static long SNAFUtoDEC(string line)
    {
        long n = 0;
        long multiplier = 1;
        for(int k = line.Length-1; k>= 0; k--)
        {
            n += multiplier*snafuDigitDictionary[line[k]]; 
            multiplier*=5;
        }

        return n;
    }

    private static string DECtoSNAFU(long v)
    {

        List<int> digits = new List<int>();
        //DEC to base 5
        while(v > 0)
        {
            int d = (int)(v%5);
            digits.Add(d);
            v/=5;
        }

        //base 5 to SNAFU
        int carry = 0;
        for(int i = 0; i < digits.Count; i++)
        {
            int d = carry + digits[i];
            if(d>2){
                d-=5;
                carry = 1;
            }else{
                carry = 0;
            }
            digits[i] = d;
        }

        if(carry > 0){
            digits.Add(carry);
        }

        //to String
        char[] snafuCharsArray = new char[digits.Count];
        for(int i = digits.Count-1, j = 0; i>= 0; i--, j++)
        {
            snafuCharsArray[j] = snafuChar[digits[i]+2];            
        }

        return new String(snafuCharsArray);
    }
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines(@"input.txt");
        long sum = 0;
        snafuDigitDictionary = new int[128]; //ASCII
        snafuDigitDictionary['2'] = 2;
        snafuDigitDictionary['1'] = 1;
        snafuDigitDictionary['0'] = 0;
        snafuDigitDictionary['-'] = -1;
        snafuDigitDictionary['='] = -2;
        foreach(string line in lines)
        {
            sum+=SNAFUtoDEC(line);
        }

        string snafuSum = DECtoSNAFU(sum);

        Console.WriteLine(snafuSum);
    }


}