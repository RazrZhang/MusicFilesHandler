using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shell32;
using System.IO;

namespace Logic
{
    public class MainLogic
    {

        /// <summary>
        /// 判断根目录是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool isPathExist(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void findFile(List<string> listFiles, DirectoryInfo directoryInfo)
        {
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            for (int i=0; i< fileInfos.Length; i++)
            {
                listFiles.Add(fileInfos[i].FullName);
            }
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            for (int i=0; i < directoryInfos.Length; i++)
            {
                findFile(listFiles, directoryInfos[i]);
            }
        }

        /// <summary>
        /// 判断文件是否为音乐文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool isMusicFile(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string targetExtension = ".mp3;.flac;.wma;.wav";
            if (targetExtension.IndexOf(extension) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 获取歌手名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string getArtist(string fileName)
        {
            try
            {
                Shell32.Shell shell = new Shell();
                Folder dir = shell.NameSpace(System.IO.Path.GetDirectoryName(fileName));
                FolderItem item = dir.ParseName(System.IO.Path.GetFileName(fileName));
                string artist = dir.GetDetailsOf(item, 20);
                return artist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取专辑名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string getAlbum(string fileName)
        {
            try
            {
                Shell32.Shell shell = new Shell();
                Folder dir = shell.NameSpace(System.IO.Path.GetDirectoryName(fileName));
                FolderItem item = dir.ParseName(System.IO.Path.GetFileName(fileName));
                string album = dir.GetDetailsOf(item, 14);
                return album;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 校验信息是否全为问号（有些歌曲文件会出现这种问题，可交由人工判断，或完成填写后使用）
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool isAllQuestionMark(string info)
        {
            bool result = true;
            for (int i=0; i<info.Length; i++)
            {
                if (info[i] != '?')
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourcefileName"></param>
        /// <param name="targetDir"></param>
        public void moveFile(string sourcefileName, string targetDir)
        {
            try
            {
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                if (!File.Exists(sourcefileName))
                {
                    throw new Exception("文件【" + sourcefileName + "】不存在，请检查");
                }
                int index = sourcefileName.LastIndexOf("\\");
                string fileName = sourcefileName.Substring(index);
                string targetDestination = targetDir + fileName;
                if (!File.Exists(targetDestination))
                {
                    File.Move(sourcefileName, targetDestination);
                    File.Delete(sourcefileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
