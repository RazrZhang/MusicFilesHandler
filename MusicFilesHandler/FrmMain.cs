using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;
using System.IO;

namespace MusicFilesHandler
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string rootPath = this.tbRootPath.Text.Trim();
            MainLogic mainLogic = new MainLogic();
            if (mainLogic.isPathExist(rootPath))
            {
                try
                {
                    List<string> listFile = new List<string>();
                    DirectoryInfo directory = new DirectoryInfo(rootPath);
                    mainLogic.findFile(listFile, directory);
                    int num = 0;
                    foreach(string fileName in listFile)
                    {
                        if (mainLogic.isMusicFile(fileName))
                        {
                            string targetPath = rootPath;
                            string artist = mainLogic.getArtist(fileName);
                            string album = mainLogic.getAlbum(fileName);
                            if (!string.IsNullOrEmpty(artist) && !mainLogic.isAllQuestionMark(artist))
                            {
                                targetPath += "\\" + artist;
                            }
                            if (!string.IsNullOrEmpty(album) && !mainLogic.isAllQuestionMark(album))
                            {
                                targetPath += "\\" + album;
                            }
                            mainLogic.moveFile(fileName, targetPath);
                            num++;
                        }
                    }
                    showSuccessMessage("移动完成,本次处理文件" + num + "个");
                }
                catch (Exception ex)
                {
                    showErrorMessage(ex.Message);
                }
            }
            else
            {
                showErrorMessage("输入的路径不存在，请确认后操作");
            }
        }

        private void showErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "出错啦", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void showSuccessMessage(string successMessage)
        {
            MessageBox.Show(successMessage, "成功了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
