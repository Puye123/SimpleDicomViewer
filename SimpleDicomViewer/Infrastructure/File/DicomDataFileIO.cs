using SimpleDicomViewer.Domain.Entities;
using SimpleDicomViewer.Domain.Repositories;
using SimpleDicomViewer.Domain.ValueObjects;
using SimpleDicomViewer.Domain.ValueObjects.VR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SimpleDicomViewer.Infrastructure.File
{
    /// <summary>
    /// DICOMファイル(バイナリ形式で拡張子が.dcmなど) へのデータ読み書きを行うクラス
    /// </summary>
    public class DicomDataFileIO : IDicomDataRepository
    {
        public IValueElementFactory Factory { get; }
        public DicomDataFileIO() {
            Factory = new ValueElementFactory();
        }
        public DicomDataFileIO(IValueElementFactory factory)
        {
            this.Factory = factory;
        }
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
                // Console.WriteLine(prefix);

                // データの読み込み
                List<ValueElement> valueElements = new List<ValueElement>();
                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    var ve = ReadValueElement(binaryReader);
                    if (ve != null)
                    {
                        valueElements.Add(ve);
                    }   
                }

                DicomDataEntity dicomDataEntity = new DicomDataEntity(valueElements);
                return dicomDataEntity;
            }
        }

        private ValueElement ReadValueElement(BinaryReader binaryReader)
        {
            // タグの読み込み
            ushort group = binaryReader.ReadUInt16();
            ushort element = binaryReader.ReadUInt16();
            Tag tag = new Tag(group, element);
            Debug.WriteLine(tag.ToString());

            if (group == 0xFFFE)
            {
                return null;
            }

            // VR種別の読み込み
            string vr = Encoding.ASCII.GetString(binaryReader.ReadBytes(2));
            Debug.WriteLine(vr);

            // 長さの読み込み
            int length;
            if (vr == "OB" || vr == "OW" || vr == "OF" || vr == "SQ" || vr == "UT" || vr == "UN")
            {
                // 4バイトの予約領域を読み飛ばす
                binaryReader.ReadUInt16();
                // 4バイトの長さを読み込む
                length = binaryReader.ReadInt32();
            }
            else
            {
                length = binaryReader.ReadInt16();
            }
            Debug.WriteLine(length);
            // 値の読み込み
            byte[] value;
            if (length > 0)
            {
                value = binaryReader.ReadBytes((int)length);
                Debug.WriteLine(BitConverter.ToString(value));
            }
            else
            {
                value = new byte[0];
            }
            Debug.WriteLine("================");

            return Factory.Create(vr, tag, value);
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
