public class LexerException : System.Exception
{
    public LexerException(string msg)
        : base(msg)
    {
    }

}

public class Lexer
{

    protected int position;
    protected char currentCh;       // очередной считанный символ
    protected int currentCharValue; // целое значение очередного считанного символа
    protected System.IO.StringReader inputReader;
    protected string inputString;

    public Lexer(string input)
    {
        inputReader = new System.IO.StringReader(input);
        inputString = input;
    }

    public void Error()
    {
        System.Text.StringBuilder o = new System.Text.StringBuilder();
        o.Append(inputString + '\n');
        o.Append(new System.String(' ', position - 1) + "^\n");
        o.AppendFormat("Error in symbol {0}", currentCh);
        throw new LexerException(o.ToString());
    }

    protected void NextCh()
    {
        this.currentCharValue = this.inputReader.Read();
        this.currentCh = (char)currentCharValue;
        this.position += 1;
    }

    public virtual void Parse()
    {

    }
}

public class IntLexer : Lexer
{

    protected System.Text.StringBuilder intString;

    public IntLexer(string input)
        : base(input)
    {
        intString = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        if (currentCh == '+' || currentCh == '-')
        {
            NextCh();
        }

        if (char.IsDigit(currentCh))
        {
            NextCh();
        }
        else
        {
            Error();
        }

        while (char.IsDigit(currentCh))
        {
            NextCh();
        }


        if (currentCharValue != -1) // StringReader вернет -1 в конце строки
        {
            Error();
        }

        System.Console.WriteLine("Integer was recognized");

    }
}


public class IntNum : Lexer
{

    protected System.Text.StringBuilder intString;

    public IntNum(string input)
        : base(input)
    {
        intString = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        string num = "";
        if (currentCh == '+' || currentCh == '-')
        {
            num += currentCh;
            NextCh();
        }

        if (char.IsDigit(currentCh))
        {
            num += currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        while (char.IsDigit(currentCh))
        {
            num += currentCh;
            NextCh();
        }


        if (currentCharValue != -1)
        {
            Error();
        }

        int i = System.Convert.ToInt32(num);
        System.Console.WriteLine(i + " was recognized");

    }
}

public class Identifier : Lexer
{

    protected System.Text.StringBuilder str;

    public Identifier(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        
        if (char.IsLetter(currentCh))
        {
            NextCh();
        }
        else
        {
            Error();
        }

        while (char.IsDigit(currentCh) || char.IsLetter(currentCh))
        {
            NextCh();
        }


        if (currentCharValue != -1)
        {
            Error();
        }

        System.Console.WriteLine("Identifier was recognized");

    }
}

public class Int0 : Lexer
{

    protected System.Text.StringBuilder str;

    public Int0(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();

        if (currentCh == '+' || currentCh == '-')
        {
            NextCh();
        }

        if (char.IsDigit(currentCh) && currentCh != '0')
        {
            NextCh();
        }
        else
        {
            Error();
        }

        while (char.IsDigit(currentCh))
        {
            NextCh();
        }


        if (currentCharValue != -1)
        {
            Error();
        }

        System.Console.WriteLine("Integer was recognized");


    }
}

public class Alternation : Lexer
{

    protected System.Text.StringBuilder str;

    public Alternation(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        char prev = ' ';

        if (char.IsLetter(currentCh))
        {
            prev = currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        while ((char.IsDigit(currentCh) && char.IsLetter(prev)) || (char.IsLetter(currentCh) && char.IsDigit(prev)))
        {
            prev = currentCh;
            NextCh();
        }


        if (currentCharValue != -1)
        {
            Error();
        }

        System.Console.WriteLine("Sequence Digit|Letter|Digit... was recognized");
    }
}

public class ListOfLetters : Lexer
{

    protected System.Text.StringBuilder str;

    public ListOfLetters(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        string letters = "";
        bool prevIsLet = true;

        if (char.IsLetter(currentCh))
        {
            letters += currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        while ((char.IsLetter(currentCh) && !prevIsLet) || ((currentCh == ';' || currentCh == ':')  && prevIsLet))
        {
            if (char.IsLetter(currentCh))
            {
                letters += currentCh;
                prevIsLet = true;
            }
            else
                prevIsLet = false;
            NextCh();
        }


        if (currentCharValue != -1)
        {
            Error();
        }

        System.Console.WriteLine("List of letters was recognized");
    }
}

public class ListOfNumbers : Lexer
{

    protected System.Text.StringBuilder str;

    public ListOfNumbers(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        string numbers = "";

        if (char.IsDigit(currentCh))
        {
            numbers += currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        while (char.IsDigit(currentCh) || (currentCharValue == ' '))
        {
            if (char.IsDigit(currentCh))
                numbers += currentCh;
            NextCh();
        }


        if (currentCharValue != -1)
        {
            Error();
        }

        System.Console.WriteLine("List of numbers was recognized");
    }
}

public class Groups : Lexer
{

    protected System.Text.StringBuilder str;

    public Groups(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        string gr = "";
        bool snd_num = false;
        bool snd_let = false;
        char prev = ' ';

        if (char.IsDigit(currentCh) || char.IsLetter(currentCh))
        {
            gr += currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        while ((char.IsDigit(currentCh) && (!snd_num)) || (char.IsLetter(currentCh) && (!snd_let)))
        {
            gr += currentCh;
            if (char.IsDigit(currentCh) && (char.IsDigit(prev)))
                snd_num = true;
            else snd_num = false;
            if (char.IsLetter(currentCh) && (char.IsLetter(prev)))
                snd_let = true;
            else snd_let = false;
            prev = currentCh;
            NextCh();
        }


        if (currentCharValue != -1)
        {
            Error();
        }

        System.Console.WriteLine("List of numbers was recognized");
    }
}

public class DoubleLexer : Lexer
{

    protected System.Text.StringBuilder str;

    public DoubleLexer(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        string d = "";
        bool was_point = false;
        char prev = ' ';

        if (char.IsDigit(currentCh))
        {
            d += currentCh;
            prev = currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        while (char.IsDigit(currentCh) || (currentCh == '.'))
        {
            d += currentCh;
            if (currentCh == '.')
                if (was_point)
                    Error();
                else
                was_point = true;
            prev = currentCh;
            NextCh();
        }


        if ((currentCharValue != -1) || (!was_point) || (prev  == '.'))
        {
            Error();
        }

        System.Console.WriteLine("Double was recognized");
    }
}

public class StringInQuotes : Lexer
{

    protected System.Text.StringBuilder str;

    public StringInQuotes(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        string s = "";

        if (currentCh == '\'')
        {
            s += currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        while (currentCharValue != -1)        
        {
            s += currentCh;
            if (currentCh == '\'')
            {
                NextCh();
                break;
            }
            NextCh();
        }
        
        if (currentCharValue != -1)
        {
            Error();
        }

        System.Console.WriteLine("String in quotes was recognized");
    }
}

public class Comment : Lexer
{

    protected System.Text.StringBuilder str;

    public Comment(string input)
        : base(input)
    {
        str = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        string s = "";
        char prev = ' ';

        if (currentCh == '/')
        {
            s += currentCh;
            prev = currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        if (currentCh == '*')
        {
            s += currentCh;
            prev = currentCh;
            NextCh();
        }
        else
        {
            Error();
        }

        while (currentCharValue != -1)
        {
            s += currentCh;
            if ((currentCh == '/') && (prev == '*'))
            {
                NextCh();
                break;
            }
            prev = currentCh;
            NextCh();
        }

        if (currentCharValue != -1)
        {
            Error();
        }

        System.Console.WriteLine("Comment was recognized");
    }
}

public class Program
{
    public static void Test()
    {
        // Тесты для IntNum
        System.Console.WriteLine("Tests for IntNum" + "\n");
        string input = "0";
        System.Console.WriteLine(input + ":");
        Lexer L = new IntNum(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "";
        System.Console.WriteLine("Empty string:");
        L = new IntNum(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "253";
        System.Console.WriteLine(input + ":");
        L = new IntNum(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "-271273";
        System.Console.WriteLine(input + ":");
        L = new IntNum(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "124-1373";
        System.Console.WriteLine(input + ":");
        L = new IntNum(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        // Тесты для Identifier
        System.Console.WriteLine("\n" + "Tests for Identifier");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new Identifier(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "a";
        System.Console.WriteLine(input + ":");
        L = new Identifier(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "bnasbh1221bhiu";
        System.Console.WriteLine(input + ":");
        L = new Identifier(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "5";
        System.Console.WriteLine(input + ":");
        L = new Identifier(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "5asbdh28";
        System.Console.WriteLine(input + ":");
        L = new Identifier(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = ";adf22f";
        System.Console.WriteLine(input + ":");
        L = new Identifier(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        // Тесты для Int0
        System.Console.WriteLine("\n" + "Tests for Int0");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new Int0(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "-724";
        System.Console.WriteLine(input + ":");
        L = new Int0(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "213651";
        System.Console.WriteLine(input + ":");
        L = new Int0(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "abc";
        System.Console.WriteLine(input + ":");
        L = new Int0(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "-012";
        System.Console.WriteLine(input + ":");
        L = new Int0(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "0";
        System.Console.WriteLine(input + ":");
        L = new Int0(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "0931";
        System.Console.WriteLine(input + ":");
        L = new Int0(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        // Тесты для Alternation
        System.Console.WriteLine("\n" + "Tests for Alternation");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new Alternation(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "a";
        System.Console.WriteLine(input + ":");
        L = new Alternation(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "1";
        System.Console.WriteLine(input + ":");
        L = new Alternation(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "b3n4n6";
        System.Console.WriteLine(input + ":");
        L = new Alternation(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "m2hr4";
        System.Console.WriteLine(input + ":");
        L = new Alternation(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "i8s74g";
        System.Console.WriteLine(input + ":");
        L = new Alternation(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }


        // Тесты для ListOfLetters
        System.Console.WriteLine("\n" + "Tests for ListOfLetters");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new ListOfLetters(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "v;d:n:";
        System.Console.WriteLine(input + ":");
        L = new ListOfLetters(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = ":n;e;";
        System.Console.WriteLine(input + ":");
        L = new ListOfLetters(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "m";
        System.Console.WriteLine(input + ":");
        L = new ListOfLetters(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "fe:n:";
        System.Console.WriteLine(input + ":");
        L = new ListOfLetters(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "m;:w;w";
        System.Console.WriteLine(input + ":");
        L = new ListOfLetters(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        // Тесты для ListOfNumbers
        System.Console.WriteLine("\n" + "Tests for ListOfNumbers");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new ListOfNumbers(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "12     636   22 1 523";
        System.Console.WriteLine(input + ":");
        L = new ListOfNumbers(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "  2 4   9";
        System.Console.WriteLine(input + ":");
        L = new ListOfNumbers(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "1; 3";
        System.Console.WriteLine(input + ":");
        L = new ListOfNumbers(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        // Тесты для Groups
        System.Console.WriteLine("\n" + "Tests for Groups");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new Groups(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "aa12c23dd1";
        System.Console.WriteLine(input + ":");
        L = new Groups(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "aa122c23dd1";
        System.Console.WriteLine(input + ":");
        L = new Groups(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "aa 12v6";
        System.Console.WriteLine(input + ":");
        L = new Groups(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        // Тесты для DoubleLexer
        System.Console.WriteLine("\n" + "Tests for DoubleLexer");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new DoubleLexer(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "3";
        System.Console.WriteLine(input + ":");
        L = new DoubleLexer(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "5241.32";
        System.Console.WriteLine(input + ":");
        L = new DoubleLexer(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "7265.27.37";
        System.Console.WriteLine(input + ":");
        L = new DoubleLexer(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = ".34";
        System.Console.WriteLine(input + ":");
        L = new DoubleLexer(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }
        
        input = "782.";
        System.Console.WriteLine(input + ":");
        L = new DoubleLexer(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        // Тесты для StringInQuotes
        System.Console.WriteLine("\n" + "Tests for StringInQuotes");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new StringInQuotes(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "'dbj2134;sdh.adsn'";
        System.Console.WriteLine(input + ":");
        L = new StringInQuotes(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "'h6gsf2'sdj1d'";
        System.Console.WriteLine(input + ":");
        L = new StringInQuotes(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "''";
        System.Console.WriteLine(input + ":");
        L = new StringInQuotes(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "'vqhw2";
        System.Console.WriteLine(input + ":");
        L = new StringInQuotes(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "annx'";
        System.Console.WriteLine(input + ":");
        L = new StringInQuotes(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        // Тесты для Comment
        System.Console.WriteLine("\n" + "Tests for Comment");
        input = "";
        System.Console.WriteLine("Empty string:");
        L = new Comment(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "/**/";
        System.Console.WriteLine(input + ":");
        L = new Comment(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "/*jbs.d3d431iu*/";
        System.Console.WriteLine(input + ":");
        L = new Comment(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "/*4odpus";
        System.Console.WriteLine(input + ":");
        L = new Comment(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "an;sjsof*/";
        System.Console.WriteLine(input + ":");
        L = new Comment(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

        input = "*/msdc;w";
        System.Console.WriteLine(input + ":");
        L = new Comment(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    public static void Main()
    {
        Test();
    }
}