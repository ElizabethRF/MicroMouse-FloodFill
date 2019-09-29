using System; 

class Maze{
  // cells 
  private Cell[,] maze;
  private int mazeSize;   

  public Maze(int mazeSize){
    this.mazeSize = mazeSize; 
    createMaze(mazeSize);
  }

  public void createMaze(int mazeSize){
    maze = new Cell[mazeSize,mazeSize];
    for(int i = 0; i < mazeSize; i++){
      for(int j = 0; j < mazeSize; j++){
        maze[i,j] = new Cell(initialValue(i,j),i,j);
        maze[i,j].setWalls(initialWalls(i,j));
      }
    }
  }


  // GETTERS 
  public Cell getCell(int x, int y){
    return maze[x,y]; 
  }

  public int getMazeSize(){
    return mazeSize; 
  }


  private int initialValue(int x, int y){
      int centerX = mazeSize/2; 
      int centerY = mazeSize/2; 
      // choose center cell 
      if(x <= centerX-1){
        centerX -=1; 
      }
      if(y <= centerY-1){
        centerY-=1;
      }

      int xDiff = Math.Abs(centerX - x); 
      int yDiff = Math.Abs(centerY - y); 
      int res = xDiff + yDiff; 
      return res;  
  }

  public void setAllWalls(){
    for(int i = 0; i < mazeSize; i++){
      for(int j = 0; j < mazeSize; j++){
        bool[] trueWalls = new bool[4]{true,true,true,true};
        maze[i,j].setWalls(trueWalls);
      }
    }
  }

  private bool[] initialWalls(int x, int y){
    bool[] initialWalls =new bool[4];
    if(x == 0){ // North
      initialWalls[0] = true; 
    } 
    if(x == mazeSize-1){ // South 
      initialWalls[1] = true; 
    } 
    if(y == mazeSize-1){ // East 
      initialWalls[2] = true; 
    } 
    if(y == 0){ // West 
      initialWalls[3] = true; 
    } 
    return initialWalls; 
  }

  public void printMazeWithValues(int mmXPos, int mmYPos){
    for(int i = 0; i < maze.GetLength(0); i++){
      for(int j = 0; j< maze.GetLength(1); j++){
        // North Wall 
        if(maze[i,j].getNorthWall()){
          Console.Write("+---");
        }
        else{ // No north wall 
          Console.Write("+   ");
        }
      }
      // End of line 
      Console.WriteLine("+");
      for(int j = 0; j < maze.GetLength(1);j++){ // west wall 
        if(maze[i,j].getWestWall()){
          Console.Write("|");
        }
        else{ // no wall 
          Console.Write(" ");
        }
        // print cell value 
        if(i==mmXPos && j == mmYPos)
        {
          Console.Write("*"+maze[i,j].getValue().ToString("D2")+"");
        }else{
          Console.Write(maze[i,j].getValue().ToString("D3")+"");
        }
        
      }
      // print last east wall 
        Console.WriteLine("|");
      
    }
    // print last south wall 
    for(int j = 0; j<maze.GetLength(1);j++){
          Console.Write("+---");
      }
      Console.WriteLine("+");
  }

  public void printMaze(){
    for(int i = 0; i < maze.GetLength(0); i++){
      for(int j = 0; j< maze.GetLength(1); j++){
        // North Wall 
        if(maze[i,j].getNorthWall()){
          Console.Write("+---");
        }
        else{ // No north wall 
          Console.Write("+   ");
        }
      }
      // End of line 
      Console.WriteLine("+");
      for(int j = 0; j < maze.GetLength(1);j++){ // west wall 
        if(maze[i,j].getWestWall()){
          Console.Write("|");
        }
        else{ // no wall 
          Console.Write(" ");
        }
        // print spaces 
        Console.Write("   ");
        
      }
      // print last east wall 
        Console.WriteLine("|");
      
    }
    // print last south wall 
    for(int j = 0; j<maze.GetLength(1);j++){
        Console.Write("+---");
      }
      Console.WriteLine("+");
     // temporal 
     /*for(int i = 0 ; i < mazeSize; i++){
      for(int j = 0; j < mazeSize; j++){
        Console.WriteLine("("+i+","+j+") N:"+ maze[i,j].getNorthWall()+" S:"+maze[i,j].getSouthWall()+" E:"+ maze[i,j].getEastWall()+" W:"+maze[i,j].getWestWall());
      }
     }*/
  }

  

}