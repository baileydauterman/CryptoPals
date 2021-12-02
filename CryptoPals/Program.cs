// See https://aka.ms/new-console-template for more information
using CryptoPals;

// Test hex to base 64
Console.WriteLine("######### SET 1 #########");
string hex = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
string output = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";

string hex2b64 = Basics.HexStringToB64(hex);

Console.WriteLine(hex2b64 == output);


// Test fixed XOR
//Console.WriteLine();
//Console.WriteLine("######### SET 2 #########");
//string input = "1c0111001f010100061a024b53535009181c";
//string key = "686974207468652062756c6c277320657965";
//string expectedOutput = "746865206b696420646f6e277420706c6179";
//output = Basics.XORString(input, key);
//string expOutputB64 = Basics.HexStringToB64(expectedOutput);

//Console.WriteLine("My Input : " + input);
//Console.WriteLine("The Key  : " + key);
//Console.WriteLine("My Output: " + output);
//Console.WriteLine("ExpOutput: " + expectedOutput);
//Console.WriteLine("ExpOutputB: " + expOutputB64);
//Console.WriteLine("Matching : " + output.CompareTo(expectedOutput));

//Single letter brute force
Console.WriteLine();
Console.WriteLine("######### SET 3 #########");
var input = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
var output2 = Basics.SingleByteBruteForce(input);

Console.WriteLine("Input: " + input);
Console.WriteLine("Decode Input: " + Basics.HexStringToB64(input));
foreach(var b in output2)
{
    Console.WriteLine(Basics.HexStringToB64(b));
}