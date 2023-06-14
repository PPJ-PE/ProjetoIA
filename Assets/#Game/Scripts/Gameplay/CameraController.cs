using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] float lerpSpeed;
        [SerializeField] float linearSpeed;
        [SerializeField] float stopDistance;

        private Vector3 initialRelativePos;

        private Vector3 PlayerRelativePos { get => initialRelativePos + player.transform.position; }
        private Vector3 HeightAdjustedPos { get => new Vector3(transform.position.x, player.transform.position.y, transform.position.z); }
        private void Start()
        {
            initialRelativePos = HeightAdjustedPos - player.transform.position;
        }
        private void LateUpdate()
        {
            Move();
        }
        private void Move()
        {
            SetXZPos(Vector3.Lerp(HeightAdjustedPos, PlayerRelativePos, lerpSpeed * Time.deltaTime));
            SetXZPos(Vector3.MoveTowards(HeightAdjustedPos, PlayerRelativePos, stopDistance));
        }
        private void SetXZPos(Vector3 newPos)
        {
            transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
        }
    }
}
