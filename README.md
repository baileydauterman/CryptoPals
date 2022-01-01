# CryptoPals -- https://cryptopals.com/

These are my attempts at the CryptoPals challenges. I am sure my code is sloppy and not the best way to implement some of the solutions, but I am using this for learning purposes. If you have any suggestions on how to better the code, feel free to leave a PR or send me a message.

### What Are The Rules?
There aren't any! For several years, we ran these challenges over email, and asked participants not to share their results. The honor system worked beautifully! But now we're ready to set aside the ceremony and just publish the challenges for everyone to work on.

### How Much Math Do I Need To Know?
If you have any trouble with the math in these problems, you should be able to find a local 9th grader to help you out. It turns out that many modern crypto attacks don't involve much hard math.

### How Much Crypto Do I Need To Know?
None. That's the point.

### So What Do I Need To Know?
You'll want to be able to code proficiently in any language. We've received submissions in C, C++, Python, Ruby, Perl, Visual Basic, X86 Assembly, Haskell, and Lisp. Surprise us with another language. Our friend Maciej says these challenges are a good way to learn a new language, so maybe now's the time to pick up Clojure or Rust.

### What Should I Expect?
Right now, we have eight sets. They get progressively harder. Again: these are based off real-world vulnerabilities. None of them are "puzzles". They're not designed to trip you up. Some of the attacks are clever, though, and if you're not familiar with crypto cleverness... well, you should like solving puzzles. An appreciation for early-90's MTV hip-hop can't hurt either.

### Can You Give Us A Long-Winded Indulgent Description For Why You'Ve Chosen To Do This?
It turns out that we can.

If you're not that familiar with crypto already, or if your familiarity comes mostly from things like Applied Cryptography, this fact may surprise you: most crypto is fatally broken. The systems we're relying on today that aren't known to be fatally broken are in a state of just waiting to be fatally broken. Nobody is sure that TLS 1.2 or SSH 2 or OTR are going to remain safe as designed.

The current state of crypto software security is similar to the state of software security in the 1990s. Specifically: until around 1995, it was not common knowledge that software built by humans might have trouble counting. As a result, nobody could size a buffer properly, and humanity incurred billions of dollars in cleanup after a decade and a half of emergency fixes for memory corruption vulnerabilities.

Counting is not a hard problem. But cryptography is. There are just a few things you can screw up to get the size of a buffer wrong. There are tens, probably hundreds, of obscure little things you can do to take a cryptosystem that should be secure even against an adversary with more CPU cores than there are atoms in the solar system, and make it solveable with a Perl script and 15 seconds. Don't take our word for it: do the challenges and you'll see.

People "know" this already, but they don't really know it in their gut, and we think the reason for that is that very few people actually know how to implement the best-known attacks. So, mail us, and we'll give you a tour of them.

## Set 1

This is the qualifying set. We picked the exercises in it to ramp developers up gradually into coding cryptography, but also to verify that we were working with people who were ready to write code.

This set is relatively easy. With one exception, most of these exercises should take only a couple minutes. But don't beat yourself up if it takes longer than that. It took Alex two weeks to get through the set!

If you've written any crypto code in the past, you're going to feel like skipping a lot of this. Don't skip them. At least two of them (we won't say which) are important stepping stones to later attacks.

1. ✔️ Convert hex to base64
2. ✔️ Fixed XOR
3. ✔️ Single-byte XOR cipher
4. ✔️ Detect single-character XOR
5. ✔️ Implement repeating-key XOR
6. ✔️ Break repeating-key XOR
7. ❌ AES in ECB mode
8. ❌ Detect AES in ECB mode


