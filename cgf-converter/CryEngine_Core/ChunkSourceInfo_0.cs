﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CgfConverter.CryEngine_Core
{
    public class ChunkSourceInfo_0 : ChunkSourceInfo
    {
        public override void Read(BinaryReader reader)
        {
            this.ChunkType = this._header.ChunkType;
            this.Version = this._header.Version;
            this.Offset = this._header.Offset;
            this.ID = this._header.ID;
            this.Size = this._header.Size;

            reader.BaseStream.Seek(this._header.Offset, 0);

            UInt32 peek = reader.ReadUInt32();

            // Try and detect SourceInfo type - if it's there, we need to skip ahead a few bytes
            if ((peek == (UInt32)ChunkTypeEnum.SourceInfo) || (peek + 0xCCCBF000 == (UInt32)ChunkTypeEnum.SourceInfo))
            {
                this.SkipBytes(reader, 12);
            }
            else
            {
                reader.BaseStream.Seek(this._header.Offset, 0);
            }

            if (this.Offset != this._header.Offset || this.Size != this._header.Size)
            {
                Utils.Log(LogLevelEnum.Warning, "Conflict in chunk definition");
                Utils.Log(LogLevelEnum.Warning, "{0:X}+{1:X}", this._header.Offset, this._header.Size);
                Utils.Log(LogLevelEnum.Warning, "{0:X}+{1:X}", this.Offset, this.Size);
                this.WriteChunk();
            }

            this.ChunkType = ChunkTypeEnum.SourceInfo; // this chunk doesn't actually have the chunktype header.
            this.SourceFile = reader.ReadCString();
            this.Date = reader.ReadCString().TrimEnd(); // Strip off last 2 Characters, because it contains a return
            this.Author = reader.ReadCString();
        }
    }
}
