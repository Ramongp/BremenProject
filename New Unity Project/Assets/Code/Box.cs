using UnityEngine;
using System.Collections;

public class Box {

	public Face Front, Back, Up, Down, Right, Left;
	public Box(Face F,Face B,Face U, Face D,Face R, Face L) {
		Front = F;
		Back = B;
		Up = U;
		Down = D;
		Right = R;
		Left = L;
	}

	public void MoveUp()
	{
		Clockwise (Right);
		Counterclockwise (Left);
		/*Front.localization = 2;
		Down.localization = 0;
		Back.localization = 3;
		Up.localization = 1;*/
		Face temp = Front;
		Front = Down;
		Down = Back;
		Back = Up;
		Up = temp;
	}
	public void MoveDown()
	{
		Clockwise (Left);
		Counterclockwise (Right);
		/*Front.localization = 3;
		Down.localization = 1;
		Back.localization = 2;
		Up.localization = 0;*/
		Face temp = Front;
		Front = Up;
		Up = Back;
		Back = Down;
		Down = temp;
	}
	public void MoveRight()
	{
		Clockwise (Down);
		Counterclockwise (Up);
		/*Front.localization=4;
		Left.localization=0;
		Back.localization=5;
		Right.localization=1;*/
		Face temp = Front;
		Front = Left;
		Left = Back;
		Back = Right;
		Right = temp;

	}
	public void MoveLeft()
	{
		Clockwise (Up);
		Counterclockwise (Down);
		/*Front.localization=5;
		Left.localization=1;
		Back.localization=4;
		Right.localization=0;*/
		Face temp = Front;
		Front = Right;
		Right = Back;
		Back = Left;
		Left = temp;
	}
	public void MoveUpRight()
	{
		Clockwise (Front);
		Counterclockwise (Back);
		/*Up.localization=4;
		Left.localization=2;
		Down.localization=5;
		Right.localization=3;*/
		Face temp = Up;
		Up = Left;
		Left = Down;
		Down = Right;
		Right = temp;

	}
	public void MoveUpLeft()
	{
		Clockwise (Back);
		Counterclockwise (Front);
		/*Up.localization=5;
		Left.localization=3;
		Down.localization=4;
		Right.localization=2;*/
		Face temp = Up;
		Up = Right;
		Right = Down;
		Down = Left;
		Left = temp;
	}

	void Clockwise(Face f)
	{
		switch (f.orientation) {

		case 0:
			f.orientation = 1;
			break;
		case 3:
			f.orientation = 0;
			break;
		case 2:
			f.orientation = 3;
			break;
		case 1:
			f.orientation = 1;
			break;
		};
	}

	void Counterclockwise(Face f)
	{
		switch (f.orientation) {

		case 0:
			f.orientation = 3;
			break;
		case 3:
			f.orientation = 2;
			break;
		case 2:
			f.orientation = 1;
			break;
		case 1:
			f.orientation = 0;
			break;
		};
	}

	public string Status ()
	{
		return ("Front: loc " + Front.localization.ToString() + " orient " + Front.orientation.ToString() + " symbol " + Front.symbol.ToString() +
			" Up: loc " + Up.localization.ToString() + " orient " + Up.orientation.ToString() + " symbol " + Up.symbol.ToString() +
			" Right: loc " + Right.localization.ToString() + " orient " + Right.orientation.ToString() + " symbol " + Right.symbol.ToString());
	}

}
