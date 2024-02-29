using SimpleDicomViewer.Domain.Entities;
using SimpleDicomViewer.Domain.Repositories;
using SimpleDicomViewer.Domain.ValueObjects;
using SimpleDicomViewer.Domain.ValueObjects.VR;
using System;
using System.IO;
using System.Text;

namespace SimpleDicomViewer.Infrastructure.File
{
    /// <summary>
    /// DICOMファイル(バイナリ形式で拡張子が.dcmなど) へのデータ読み書きを行うクラス
    /// </summary>
    public class DicomDataFileIO : IDicomDataRepository
    {
        /// <summary>
        /// DICOMデータの読み込み
        /// </summary>
        /// <param name="pathString">読み込み元のパス文字列</param>
        /// <returns>DICOMデータエンティティ</returns>
        public DicomDataEntity Read(string pathString)
        {
            using (FileStream fileStream = new FileStream(pathString, FileMode.Open))
            using (BinaryReader binaryReader = new BinaryReader(fileStream))
            {
                // 先頭128バイトを読みとばす
                byte[] _ = binaryReader.ReadBytes(128);

                // プレフィックス これは DICM なはず
                string prefix = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
                Console.WriteLine(prefix);

                // データの読み込み
                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    ReadValueElement(binaryReader);
                }
                return null;
            }
        }

        private ValueElement ReadValueElement(BinaryReader binaryReader)
        {
            // タグの読み込み
            ushort group = binaryReader.ReadUInt16();
            ushort element = binaryReader.ReadUInt16();
            Console.WriteLine(group + ":" + element);
            Tag tag = new Tag(group, element);

            // VR種別の読み込み
            string vr = Encoding.ASCII.GetString(binaryReader.ReadBytes(2));
            Console.WriteLine(vr);

            // 長さの読み込み
            uint length;
            if (vr == "OB" || vr == "OW" || vr == "OF" || vr == "SQ" || vr == "UT" || vr == "UN")
            {
                // 4バイトの予約領域を読み飛ばす
                binaryReader.ReadUInt16();
                // 4バイトの長さを読み込む
                length = binaryReader.ReadUInt32();
            }
            else
            {
                length = binaryReader.ReadUInt16();
            }
            Console.WriteLine(length);
            // 値の読み込み
            byte[] value = binaryReader.ReadBytes((int)length);
            Console.WriteLine(BitConverter.ToString(value));

            Console.WriteLine("================");

            // Todo: ドメインレイヤにファクトリクラスを作ってここでインスタンス正史江

            return null;
        }

        /// <summary>
        /// DICOMデータの書き込み
        /// </summary>
        /// <param name="pathString">書き込み先のパス文字列</param>
        /// <param name="dicomData">DICOMデータエンティティ</param>
        public void Write(string pathString, DicomDataEntity dicomData)
        {
            throw new NotImplementedException();
        }
    }
}
