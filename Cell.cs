using System; 

class Cell{
  private bool[] walls; 
  private int value; 
  private bool visited; 
  private int xCoord; 
  private int yCoord; 

  public Cell(int value, int x, int y){
    walls = new bool[4]; 
    this.value = value; 
    visited = false; 
    xCoord = x; 
    yCoord = y; 
  }

  // GETTERS 
  public bool[] getWalls(){
    return walls;
  }

  public int getValue(){
    return value;
  }

  public bool getVisited(){
    return visited; 
  }

  public int getXCoord(){
    return xCoord;
  }

  public int getYCoord(){
    return yCoord;
  }


  //get specific wall 
  public bool getNorthWall(){
    return walls[0];
  }

  public bool getSouthWall(){
    return walls[1];
  }

  public bool getEastWall(){
    return walls[2];
  }

  public bool getWestWall(){
    return walls[3];
  }

  // SETTERS 
  public void setWalls(bool[] walls){
    this.walls = walls;
  }

  public void setValue(int value){
    this.value = value; 
  }
  
  public void setVisited(){
    visited = true; 
  }


  // set specific wall 
  public void setNorthWall(bool wall){
    walls[0] = wall; 
  }

  public void setSouthWall(bool wall){
    walls[1] = wall; 
  }

  public void setEastWall(bool wall){
    walls[2] = wall; 
  }

  public void setWestWall(bool wall){
    walls[3] = wall; 
  }

}