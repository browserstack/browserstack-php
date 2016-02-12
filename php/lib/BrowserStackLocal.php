<?php

namespace BrowserStack;

use Exception;

error_reporting(1);

class BrowserStackLocal {

  private $handle = NULL;
  private $pipes = array();

	public function __construct($key) {
    $this->key = $key;
  }

	public function __destruct() {
        echo "";
  }

 	public function verbose() {
    	$this->verbose_flag = "-v";
  }

	public function enable_folder($path = "") {
  	$this->folder_flag = "-f";
  	$this->folder_path = $path;
	}

  public function enable_force() {
    $this->force_flag = "-force";
  }

  public function enable_only() {
    $this->only_flag = "-only";
  }

  public function enable_only_automate() {
    $this->only_automate_flag = "-onlyAutomate";
  }

  public function enable_force_local() {
    $this->force_local_flag = "-forcelocal";
  }

  public function set_local_identifier($localIdentifier = "") {
    $this->local_identifier_flag = "-localIdentifier $localIdentifier";
  }

  public function start() {

    $descriptorspec = array(
      0 => array("pipe", "r"),  // stdin is a pipe that the child will read from
      1 => array("pipe", "w"),  // stdout is a pipe that the child will write to
      2 => array("file", "/tmp/error-output.txt", "a") // stderr is a file to write to
    );
    
    $call = $this->command();

    if ($this->is_windows()) 
       $this->handle = popen("start /b .$call.", "r");
    else     
      $this->handle = proc_open($call, $descriptorspec,$this->pipes);
    
    
    while(!feof($this->pipes[1])) {
        $buffer = fgets($this->pipes[1]);
        if (preg_match("/\bError\b/i", $buffer,$match)) {
          throw new BrowserStackLocalException($buffer);
          proc_terminate($this->handle);
          return;
        }
        elseif (strcmp(rtrim($buffer),"Press Ctrl-C to exit") == 0)
          return;
        flush();    
    }

  }

  public function stop() {
    if (is_null($this->handle))
      return;
    else {
      proc_terminate($this->handle);
    }
  }

  public function command() {
    return "./BrowserStackLocal $this->folder_flag $this->key $this->folder_path $this->force_local_flag $this->local_identifier_flag $this->only_flag $this->only_automate_flag $this->force_flag $this->verbose_flag";
  }

  private function is_windows(){
    if(PHP_OS == "WINNT" || PHP_OS == "WIN32"){
        return true;
    }
    return false;
  }

}

