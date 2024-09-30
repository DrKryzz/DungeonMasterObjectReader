Most of the code is taken from the wild based on a Mark Nelson - article:
https://marknelson.us/posts/2011/11/08/lzw-revisited.html

From what i can read the LZW compression used in DM is based on LZW.d variant
With a special feature of the position 256 in the std initialized dictionary, used as a special marker of some kind.

What LZW does:
LZW compression converts strings of symbols into integer codes. 
Decompression converts codes back into strings, returning the same text that we started with.

LZW is a greedy algorithm - it tries to find the longest possible string that it has a code for, then outputs that string.

Basic algorithm:
First, both the encoder and decoder initialize the dictionary with all possible single digit strings. 
For the compressor, that looks like this:

for ( int i = 0 ; i < 256 ; i++ )
    codes[(char)i)] = i;

Then comes the key component of the LZW algorithm. 
It keeps adding input symbols to a string until It finds a string that isn’t in the dictionary. 
This string has the characteristic of being composed of a string that currently exists in the dictionary, with one additional character.
Example:
A   1
B   2
C   3
AB  4   -> A + B
AC  5   -> A + C

LZW then takes that new string and adds it to the dictionary, creating a new code. 
The strings are added to the table with code values that increment by one with each new entry (4 and 5 in the above example).

COMPRESSION ALGORITHM C++:
void compress( input_stream in, output_stream out )
{
  std::unordered_map<std::string,unsigned int> codes;
  for ( unsigned int i = 0 ; i < 256 ; i++ )
    codes[std::string(1,(char)i)] = i;
  unsigned int next_code = 257;
  std::string current_string;
  char c;
  while ( in >> c ) {
    current_string = current_string + c;
    if ( codes.find(current_string) == codes.end() ) {
      codes[ current_string ] = next_code++;
      current_string.erase(current_string.size()-1);
      out << codes[current_string];
      current_string = c;
    }
  }
  out << codes[current_string];
}

Complete example:
"Just to make sure this all adds up, 
I’ll walk through the steps the encoder takes as it processes a string from a simple two letter alphabet: ABBABBBABBA. 
There are a lot of steps shown below, but working through the process in detail is a great way to be sure you understand it:"

Input	Action(s)							New			Output
Symbol										Code		Code
A	read 'A' - set current_string to 'A'		
	'A' is in the dictionary, so continue		

B	read 'B' - set current_string to 'AB'	257 (AB)	65 (A)
	'AB' is not in the dictionary, 
	add it with code 257		
	output the code for 'A' - 65		
	set current_string to 'B'		

B	read 'B' - set current_string to 'BB'	258 (BB)	66 (B)
	'BB' is not in the dictionary, 
	add it with code 258		
	output the code for 'B' - 66		
	set current_string to 'B'		

A	read 'A' - set current_string to 'BA'	259 (BA)	66 (B)
	'BA' is not in the dictionary - 
	add it with code 259		
	output the code for 'B' - 66		
	set current_string to 'A'		

B	read 'B' - set current_string to 'AB'		
	'AB' is in the dictionary, so continue		

B	read 'B' - set current_string to 'ABB'	260 (ABB)	257 (AB)
	'ABB' is not in the dictionary - 
	add it with code 260		
	output the code for 'AB' - 257		
	set current_string to 'B'		

B	read 'B' - set current_string to 'BB'		
	'BB' is in the dictionary, so continue		

A	read 'A' - set current_string to 'BBA'	261 (BBA)	258 (BB)
	'BBA' is not in the dictionary - 
	add it with code 261		
	output the code for 'BB' - 258		
	set current_string to 'A'		

B	read 'B' - set current_string to 'AB'		
	'AB' is in the dictionary, so continue		

B	read 'B' - set current_string to 'ABB'		
	'ABB' is in the dictionary, so continue		

A	read 'A' - set current_string to 'ABBA'	262 (ABBA)	260 (ABB)
	'ABBA' is not in the dictionary - 
	add it with code 262		
	output the code for 'ABB' - 260		
	set current_string to 'A'		

EOF	end of the input stream - exit loop		65 (A)
	current string is 'A'		
	output the code for 'A' - 65		

The LZW Decoder C++:
The change made to the basic encoder to accommodate the LZW algorithm was really very simple. 
One small batch of code that initializes the dictionary, and another few lines of code to add every new unseen string to the dictionary.

As you might suspect, the changes to the decoder will be fairly simple as well. 
The first change is that the dictionary must be initialized with the same 256 single-symbol strings that the encoder uses.

Once the decoder starts running, each time it reads in a code, it must add a new value to the dictionary. 
And what is that value? The entire content of the previously decoded string, plus the first letter of the currently decoded string. 
This is exactly what the encoder does to create a new string, and the decoder must following the same steps:

void decompress( input_stream in, output_stream out )
{
  std::unordered_map<unsigned int,std::string> strings;
  for ( int unsigned i = 0 ; i < 256 ; i++ )
    strings[i] = std::string(1,i);
  std::string previous_string;
  unsigned int code;
  unsigned int next_code = 257;
  while ( in >> code ) {
    out << strings[code];
    if ( previous_string.size() )
      strings[next_code++] = previous_string + strings[code][0];
    previous_string = strings[code];
  }
}

The important thing is to understand the logic behind the decoder. 
When the encoder encounters a string that isn’t in the dictionary, 
it breaks it into two pieces: a root string and an appended character. 
It outputs the code for the root string, and adds the root string + appended character to the dictionary. 
It then starts building a new string that starts with the appended character.

So every time the decoder uses a code to extract a string from the dictionary, 
it knows that the first character in that string was the appended character of the string just added to the dictionary by the encoder. 
And the root of the string added to the dictionary? That was the previously decoded string. This line of code implements that logic:

    strings[next_code++] = previous_string + strings[code][0];
It adds a new string to the dictionary, composed of the previously seen string, and the first character of the current string. 
Thus, the decoder is adding strings to the dictionary just one step behind the encoder.

You might note one curious point in the decoder. 
Instead of always adding the string to the dictionary, it is only done conditionally:

if ( previous_string.size() )
  strings[next_code++] = previous_string + strings[code][0];
The only time that previous_string.size() is 0 is on the very first pass through the loop. 
And on the first pass through the loop, we don’t have a previous string yet, so the decoder can’t build a new dictionary entry. 
Again, the decoder is always one step behind the encoder, which is a key point in the next section, which puts the final touches on the algorithm.


The Catch
So far the LZW algorithm we’ve seen seems very elegant - that’s a characteristic we associate with algorithms that can be expressed in just a few lines of code.

Unfortunately, there is one small catch in this perceived elegance - the algorithm as I’ve shown it to you has a bug.

The bug in the algorithm relates to the fact that the encoder is always one step ahead of the decoder. When the encoder adds a string with code N to the table, 
it sends enough information to the decoder to allow the decoder to figure out the value of the string denoted by code N-1. 
The decoder won’t know what the value of the string corresponding to code N is until it receives code N+1.

This makes sense if you recall the key line of code from the decoder. 
It calculates the value of the string encoded by N-1 by looking at the string it received on the previous iteration, plus the first character of the current string. 
And that current string is the one that was sent after encoding N.

So how can this get us in trouble? The encoder is always one entry ahead of the decoder - it has entry N in its dictionary, and the decoder has entry N-1. 
So if the encoder ever sends code N, the decoder will look in its table and come up empty-handed, unable to do its job of decoding.

A simple example will show you how this can happen. 
Let’s look at the state of the encoder after it has sent the first five symbols in a stream: ABABA:

Input
Symbol	Action(s)	New
Code	Output
Code
A
read 'A' - set current_string to 'A'
'A' is in the dictionary, so continue	 	 
B
read 'B' - set current_string to 'AB'
'AB' is not in the dictionary, add it with code 257
output the code for 'A' - 65
set current_string to 'B'	257 (AB)	65 (A)
A
read 'A' - set current_string to 'BA'
'BA' is not in the dictionary, add it with code 258
output the code for 'B' - 66
set current_string to 'A'	258 (BA)	66 (B)
B
read 'B' - set current_string to 'AB'
'AB' is in the dictionary, so continue	 	 
A
read 'A' - set current_string to 'ABA'
'ABA' is not in the dictionary, add it with code 259
output the code for 'AB' - 257
set current_string to 'A'	259 (ABA)	257 (AB)
Now we are set for trouble. The encoder has symbol 259 in its dictionary, while the decoder has only gotten to 258. 
If the encoder were to send a code of 259 for its next output, the decoder would not be able to find it in its dictionary. Can this happen?

Yes, if the next two characters in the stream are BA, the next code output by the encoder will be 259, and the decoder will be lost.

In general, this can happen when a dictionary entry exists that consists of a string plus a character, and the encoder encounters the sequence string+character+string+character+string. 
In the example above, the value of string is A, and the value of character is B. After the encoder counters AB, 
it has string+character in the dictionary, so if the following sequence is ABABA, we will emit code N. 
(This can happen at the start of a file, when the string is empty, if you simply have a string of three identical characters.)

Whether this is likely to happen or not is not too important, what is important is that it most definitely can happen, and the decoder has to be aware of it. 
And it will happen repeatedly in the pathological case: a stream that consists of a single symbol, repeated on end.

The good news is that the problem is easily solved. When the decoder receives a code, and finds that this code is not present in its dictionary, 
it knows right away that the code must be the one that it will add next to its decoder. And because this only happens when we are encoding the sequence discussed above, 
the decoder knows that instead of using this value for that code:

strings[next_code++] = previous_string + strings[code][0];
it can instead use this value:

strings[ code ] = previous_string + previous_string[0];
The result of this is the insertion of just two lines of code at the start of the decompress loop, giving a loop that now looks like this:

while ( in >> code ) {
  if ( strings.find( code ) == strings.end() ) 
    strings[ code ] = previous_string + previous_string[0];
  out << strings[code];
  if ( previous_string.size() )
    strings[next_code++] = previous_string + strings[code][0];
  previous_string = strings[code];
}
And with that, you have a complete implementation of the LZW encoder and decoder.

Implementation
Now that I’ve shown you the algorithm, the next step is to take that code and add turn it into a working program. Without changing the algorithm itself, I’m going to take you through four different customizations that work as follows:

LZW-A reads and writes code values rendered in text mode, which is great for debugging. It means you can view the output of the encoder in a text editor.
LZW-B reads and writes code values as 16-bit binary integers. This is fast and efficient, and usually results in significant data compresion.
LZW-C reads and writes code values as N-bit binary integers, where N is determined by the maximum code size. 
	  Performing I/O on codes that are not aligned on byte boundaries complicates the code somewhat, but allows for greater efficiency and better compression.
LZW-D reads and writes code values as variable-length binary integers, starting with 9-bit codes and gradually increasing as the dictionary grows. This gives the maximum compression.