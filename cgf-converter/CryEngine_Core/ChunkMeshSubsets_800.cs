﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CgfConverter.CryEngine_Core
{
    public class ChunkMeshSubsets_800 : ChunkMeshSubsets
    {
        public override void Read(BinaryReader b)
        {
            base.Read(b);

            this.Flags = b.ReadUInt32();   // Might be a ref to this chunk
            this.NumMeshSubset = b.ReadUInt32();  // number of mesh subsets
            this.SkipBytes(b, 8);
            this.MeshSubsets = new MeshSubset[NumMeshSubset];
            for (Int32 i = 0; i < NumMeshSubset; i++)
            {
                this.MeshSubsets[i].FirstIndex = b.ReadUInt32();
                this.MeshSubsets[i].NumIndices = b.ReadUInt32();
                this.MeshSubsets[i].FirstVertex = b.ReadUInt32();
                this.MeshSubsets[i].NumVertices = b.ReadUInt32();
                this.MeshSubsets[i].MatID = b.ReadUInt32();
                this.MeshSubsets[i].Radius = b.ReadSingle();
                this.MeshSubsets[i].Center.x = b.ReadSingle();
                this.MeshSubsets[i].Center.y = b.ReadSingle();
                this.MeshSubsets[i].Center.z = b.ReadSingle();
            }
        }
    }
}
