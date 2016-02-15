require 'net/http'
require 'rbconfig'

class BrowserStackLocal
  attr_reader :pid

  def initialize(key, binary_path = nil)
    @key = key
    @binary_path = if binary_path.nil?
        LocalBinary.new.download
      else
        binary_path
      end
  end

  def add_args(key, value=nil)
    if key == "v"
      @verbose_flag = "-v"
    elsif key == "force"
      @force_flag = "-force"
    elsif key == "only"
      @only_flag = "-only"
    elsif key == "onlyAutomate"
      @only_automate_flag = "-onlyAutomate"
    elsif key == "forcelocal"
      @force_local_flag = "-forcelocal"
    elsif key == "localIdentifier"
      @local_identifier_flag = "-localIdentifier '#{value}'"
    elsif key == "f"
      @folder_flag = "-f"
      @folder_path = "'#{value}'"
    elsif key == "proxyHost"
      @proxy_host = "-proxyHost '#{value}'"
    elsif key == "proxyPort"
      @proxy_port = "-proxyPort #{value}"
    elsif key == "proxyUser"
      @proxy_user = "-proxyUser '#{value}'"
    elsif key == "proxyPass"
      @proxy_pass = "-proxyPass '#{value}'"
    elsif key == "hosts"
      @hosts = value
    end
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
    "#{@binary_path} #{@folder_flag} #{@key} #{@folder_path} #{@force_local_flag} #{@local_identifier_flag} #{@only_flag} #{@only_automate_flag} #{@proxy_host} #{@proxy_port} #{@proxy_user} #{@proxy_pass} #{@force_flag} #{@verbose_flag} #{@hosts}".strip
  end
end

class BrowserStackLocalException < Exception
end

class LocalBinary
  def initialize
    host_os = RbConfig::CONFIG['host_os']
    @http_path = case host_os
    when /mswin|msys|mingw|cygwin|bccwin|wince|emc/
      @windows = true
      "https://s3.amazonaws.com/browserStack/browserstack-local/BrowserStackLocal.exe"
    when /darwin|mac os/
      "https://s3.amazonaws.com/browserStack/browserstack-local/BrowserStackLocal-darwin-x64"
    when /linux/
      if 1.size == 8
        "https://s3.amazonaws.com/browserStack/browserstack-local/BrowserStackLocal-linux-x64"
      else
        "https://s3.amazonaws.com/browserStack/browserstack-local/BrowserStackLocal-linux-ia32"
      end
    end
  end

  def download
    dest_parent_dir = File.join(File.expand_path('~'), '.browserstack')
    unless File.exists? dest_parent_dir
      Dir.mkdir dest_parent_dir
    end
    res = Net::HTTP.get(@http_path, :use_ssl => true)
    binary_path = File.join(dest_parent_dir, "BrowserStackLocal#{".exe" if @windows}")
    open(binary_path) do |file|
      file.write(res.body)
    end
    binary_path
  end
end
