require 'net/http'

class BrowserStackLocal
  attr_reader :pid

  def initialize(key)
    @key = key
  end

  def enable_verbose
    @verbose_flag = "-v"
  end

  def set_folder(path)
    @folder_flag = "-f"
    @folder_path = "'#{path}'"
  end

  def enable_force
    @force_flag = "-force"
  end

  def enable_only
    @only_flag = "-only"
  end

  def enable_only_automate
    @only_automate_flag = "-onlyAutomate"
  end

  def enable_force_local
    @force_local_flag = "-forcelocal"
  end

  def set_local_identifier(local_identifier)
    @local_identifier_flag = "-localIdentifier '#{local_identifier}'"
  end

  def set_proxy(host, port, username, password)
    @proxy = "-proxyHost '#{host}' -proxyPort #{port} -proxyUser '#{username}' -proxyPass '#{password}'"
  end

  def start
    @process = IO.popen(command, "w+")

    while true
      line = @process.readline
      break if line.nil?
      if line.match(/\*\*\* Error\:/)
        @process.close
        raise BrowserStackLocalException.new(line)
        return
      end
      if line.strip == "Press Ctrl-C to exit"
        @pid = @process.pid
        return
      end
    end

    while true
      resp = Net::HTTP.get(URI.parse("http://localhost:45691/check")) rescue nil
      puts resp
      break if resp && resp.match(/running/i)
      sleep 1
    end
  end

  def stop
    return if @pid.nil?
    Process.kill("INT", @pid)
    @process.close
  end

  def command
    "BrowserStackLocal #{@folder_flag} #{@key} #{@folder_path} #{@force_local_flag} #{@local_identifier_flag} #{@only_flag} #{@only_automate_flag} #{@proxy} #{@force_flag} #{@verbose_flag}".strip
  end
end

class BrowserStackLocalException < Exception
end
