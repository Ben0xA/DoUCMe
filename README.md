# DoUCMe
This leverages the NetUserAdd Win32 API to create a new machine account. This is done by setting the usri1_priv of the USER_INFO_1 type to 0x1000. 

The primary goal is to avoid the normal detection of new user created events (4720).

This will hide the user in the Control Panel and the lusrmgr.msc Snap In. It will show up in the Group Listing, but not as a user.

net user will show the user, if it does not end with a $.
Get-LocalUser will show the user every time. Nice job Jeffrey Snover!

# Usage
Run this in Visual Studio. Change the username to what you want to use. Some options are to specify the HOSTNAME followed by a $ to blend in with the machine account name.

Also, use Homoglyphs! There is a built in Homoglyph with the Administrator account.

# Detection
Look for event ID 4741 - New Machine Account Added
Followed by 4722 and 4742
