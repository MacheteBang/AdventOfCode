using System.Security.Cryptography;

string secretKey = "yzbqklnj";
string prefix = "000000";

using MD5 md5 = MD5.Create();

int answer = 0;

for (int i = 0; i <= int.MaxValue; i++)
{
    string thisKey = $"{secretKey}{i}";
    byte[] bytes = System.Text.Encoding.ASCII.GetBytes(thisKey);
    var hash = md5.ComputeHash(bytes);

    if (Convert.ToHexString(hash).StartsWith(prefix))
    {
        answer = i;
        break;
    }
}


Console.WriteLine($"Number is {answer}");

Console.WriteLine("End of Program");