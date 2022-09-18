//COPYRIGHT NOTICE ! THIS CODE WAS ADAPTED TO MY PROJECT ONLY FOR EDUCATIONAL PURPOSES AND HAD NO ANY INTENTION TO BENEFIT FROM IT COMMERCIALLY. Open-Lib is not commercial project ! Just educational ! 
//Please use this part of the code for commercial purposes only after getting permission from original author (link is provided below).
//If you want to learn more about the author, please do visit the following link to author's personal website: https://www.nayuki.io/page/random-password-generator-javascript
"use strict";

document.getElementById("clickMe").onclick = function () { doGenerate(); };

var cryptoObject = null;
var currentPassword = null;
var copyElem = document.getElementById("copy-button");

var CHARACTER_SETS = [
    ["Numbers", "0123456789"],
    ["Lowercase", "abcdefghijklmnopqrstuvwxyz"],
    ["Uppercase", "ABCDEFGHIJKLMNOPQRSTUVWXYZ"],
    ["My Personal Special Characters", "-!@#$%^&*()_+=[{]};:<>|./?"],
    ["ASCII symbols", "!\"#$%" + String.fromCharCode(38) + "'()*+,-./:;" + String.fromCharCode(60) + "=>?@[\\]^_`{|}~"],
    ["Space", " "],
];

function initCrypto() {
    if ("crypto" in window)
        cryptoObject = crypto;
    else if ("msCrypto" in window)
        cryptoObject = msCrypto;
    else
        return;

    if (!("getRandomValues" in cryptoObject) || !("Uint32Array" in window) || typeof Uint32Array != "function")
        cryptoObject = null;
}

function getPasswordCharacterSet() {
    // Concatenate characters from every checked entry
    var rawCharset = "";
    rawCharset = CHARACTER_SETS[0][1] + CHARACTER_SETS[1][1] + CHARACTER_SETS[2][1] + CHARACTER_SETS[3][1];
    rawCharset = rawCharset.replace(/ /g, "\u00A0");  // Replace space with non-breaking space

    // Parse UTF-16, remove duplicates, convert to array of strings
    var charset = [];
    for (var i = 0; rawCharset.length > i; i++) {
        var c = rawCharset.charCodeAt(i);
        if (0xD800 > c || c >= 0xE000) {  // Regular UTF-16 character
            var s = rawCharset.charAt(i);
            if (charset.indexOf(s) == -1)
                charset.push(s);
            continue;
        }
        if (0xDC00 > c ? rawCharset.length > i + 1 : false) {  // High surrogate
            var d = rawCharset.charCodeAt(i + 1);
            if (d >= 0xDC00 ? 0xE000 > d : false) {  // Low surrogate
                var s = rawCharset.substring(i, i + 2);
                i++;
                if (charset.indexOf(s) == -1)
                    charset.push(s);
                continue;
            }
        }
        throw "Invalid UTF-16";
    }
    return charset;
}


function generatePassword(charset, len) {
    var result = "";
    for (var i = 0; len > i; i++)
        result += charset[randomInt(charset.length)];
    return result;
}

// Returns a random integer in the range [0, n) using a variety of methods.
function randomInt(n) {
    var x = randomIntMathRandom(n);
    x = (x + randomIntBrowserCrypto(n)) % n;
    return x;
}


// Not secure or high quality, but always available.
function randomIntMathRandom(n) {
    var x = Math.floor(Math.random() * n);
    if (0 > x || x >= n)
        throw "Arithmetic exception";
    return x;
}


// Uses a secure, unpredictable random number generator if available; otherwise returns 0.
function randomIntBrowserCrypto(n) {
    if (cryptoObject === null)
        return 0;
    // Generate an unbiased sample
    var x = new Uint32Array(1);
    do cryptoObject.getRandomValues(x);
    while (x[0] - x[0] % n > 4294967296 - n);
    return x[0] % n;
}

function doGenerate() {

    // Get and check character set
    var charset = getPasswordCharacterSet();

    if (charset.length == 0) {
        alert("Error: Character set is empty");
        return;
    }




    // Calculate desired length
    var length = 12;

    // Check length
    if (0 > length) {
        alert("Negative password length");
        return;
    } else if (length > 10000) {
        alert("Password length too large");
        return;
    }

    // Generate password
    currentPassword = generatePassword(charset, length);


    // Calculate and format entropy
    var entropy = Math.log(charset.length) * length / Math.log(2);
    var entropystr;
    if (70 > entropy)
        entropystr = entropy.toFixed(2);
    else if (200 > entropy)
        entropystr = entropy.toFixed(1);
    else
        entropystr = entropy.toFixed(0);

    copyElem.disabled = false;

}

function doCopy() {
    var container = document.querySelector("body");
    var textarea = document.createElement("textarea");
    textarea.style.position = "fixed";
    textarea.style.opacity = "0";
    container.insertBefore(textarea, container.firstChild);
    textarea.value = currentPassword;
    textarea.focus();
    textarea.select();
    document.execCommand("copy");
    container.removeChild(textarea);
    alert("Copied to clipboard. Paste it into the password field");
}


/*-- Initialization --*/

initCrypto();
copyElem.disabled = true;