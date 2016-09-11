This is a flight simulator with procedurally generated terrain, built by Hugh Edwards (584183) and Daniel Porteous (696965) for the University of Melbourne Graphics and Interaction assignment 1. It was completed on 11 September 2016. It was made with Unity.

-- Description

	Upon each instantiation of the program, terrain is randomly generated according to the diamond square algorithm. Terrain texture corresponds to its height, and semi-transparent water is present. The player controls a flying camera. If the camera collides with the terrain or water, it 'crashes' and is reset to its starting position. The sun also moves around the terrain (speeding up when below the horizon to make the nights short!).

-- Controls

	Esc - Toggle mouse display (to easily exit the game)
	Shift - Speed boost
	W - Forward movement
	A - Left movement
	S - Back movement
	D - Right movement
	Mouse - Camera left/right/up/down
	E,Q - Camera roll
	

-- Resources

	The .gitignore file was taken from from https://github.com/github/gitignore/blob/master/Unity.gitignore

	The diamond square algorithm was implemented as described by Wikipedia (https://en.wikipedia.org/wiki/Diamond-square_algorithm).