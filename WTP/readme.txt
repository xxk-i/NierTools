C#
Compiled program found in bin/Release

Note:
    This tool cannot be used to import new textures into Nier, only replace existing ones.
   
To use, unpack the .dtt file with the textures you want to edit using
the dat_unpacker.py found in the nier2blender repo. That unpacker is desireable
as it also unpacks any .wtp files inside to a numbered list of textures.
That order is required as a .wta is not created by this app.

*IMPORTANT*
After editing the textures, you MUST ensure that the ending file size is
the same as the original or else expect your game to crash. If it is short,
padding the dds with 0's is optimal.

You can repack the output wtp into the dat with my DATrepacker and
load it into your game with Nier:Explorer or the folder method.

This app is as bare as possible and almost obselete. I may add .wta verification.