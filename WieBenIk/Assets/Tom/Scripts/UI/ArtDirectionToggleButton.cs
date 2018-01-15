﻿using UnityEngine;
using WieBenIk.Data;

namespace WieBenIk.UI
{
    public class ArtDirectionToggleButton : MonoBehaviour
    {
        [SerializeField]
        private EArtDirections _ArtDirection;
        public EArtDirections ArtDirection
        {
            get { return _ArtDirection; }
        }
    }
}