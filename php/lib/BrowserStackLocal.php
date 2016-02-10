<?php

namespace BrowserStack;

use Exception;

error_reporting(0);

class BrowserStackLocal {

	public $key; 
	public $binaryPath; 
  
	public function __construct($key) {
    	$key = $key;
      echo $key;
	}

	public function __destruct() {
        echo "";
  }

 	public static function verbose() {
    	$verbose_flag = "-v";
  }

	public static function enable_folder($path) {
  	$folder_flag = "-f";
  	$folder_path = "$path";
	}

  public static function enable_force() {
    $force_flag = "-force";
  }

  public static function enable_only() {
    $only_flag = "-only";
  }

  public static function enable_only_automate() {
    $only_automate_flag = "-onlyAutomate";
  }

  public static function enable_force_local() {
    $force_local_flag = "-forcelocal";
  }

  public static function set_local_identifier($localIdentifier) {
    $local_identifier_flag = "-localIdentifier {$localIdentifier}";
  }

  // public static function start() {
  //   $process = IO.popen(command, "w+")

  //   while true
  //     line = $process.readline
  //     break if line.nil?
  //     if line.match(/\*\*\* Error\:/)
  //       $process.close
  //       raise BrowserStackLocalException.new(line)
  //       return
  //     }
  //     if line.strip == "Press Ctrl-C to exit"
  //       $pid = $process.pid
  //       return
  //     }
  //   }
  // }

  // public static function stop() {
  //   return if $pid.nil?
  //   Process.kill("INT", $pid)
  //   $process.close
  // }

  public static function command() {
   echo "BrowserStackLocal {$folder_flag} {$key} {$folder_path} {$force_local_flag} {$local_identifier_flag} {$only_flag} {$only_automate_flag} {$force_flag} {$verbose_flag}";#.strip
  }
}
