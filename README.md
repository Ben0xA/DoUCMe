# DoUCMe
This leverages the NetUserAdd Win32 API to create a new machine account. This is done by setting the usri1_priv of the USER_INFO_1 type to 0x1000.

# Usage
Run this in Visual Studio. Change the username to what you want to use. Some options are to specify the HOSTNAME followed by a $ to blend in with the machine account name.

Also, use Homoglyphs! There is a built in Homoglyph with the Administrator account.

# Detection
Look for event ID 4741 - New Machine Account Added
Followed by 4722 and 4742
