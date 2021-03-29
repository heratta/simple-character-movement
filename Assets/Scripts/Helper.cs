using UnityEngine;

namespace helab
{
    public static class Helper
    {
        public static Vector3 GetMoveDirectionByInput()
        {
            var dir = Vector3.zero;
            dir += GetMoveDirectionByKeyboard(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
            dir += GetDirectionByJoystick("Horizontal_L", "Vertical_L");

            return dir.normalized;
        }

        public static Vector3 GetCameraRotateValueByInput()
        {
            var dir = Vector3.zero;
            dir += GetMoveDirectionByKeyboard(
                KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
            dir += GetDirectionByJoystick("Horizontal_R", "Vertical_R");

            return dir.normalized;
        }

        private static Vector3 GetMoveDirectionByKeyboard(KeyCode forward, KeyCode back, KeyCode left, KeyCode right)
        {
            var dir = Vector3.zero;

            if (Input.GetKey(forward))
            {
                dir += Vector3.forward;
            }

            if (Input.GetKey(back))
            {
                dir += Vector3.back;
            }

            if (Input.GetKey(left))
            {
                dir += Vector3.left;
            }

            if (Input.GetKey(right))
            {
                dir += Vector3.right;
            }

            return dir;
        }

        private static Vector3 GetDirectionByJoystick(string xAxisName, string yAxisName)
        {
            var dir = Vector3.zero;

            if (!string.IsNullOrEmpty(xAxisName))
            {
                float xAxis = Input.GetAxis(xAxisName);
                if (0.5f < Mathf.Abs(xAxis))
                {
                    dir += new Vector3(xAxis, 0f, 0f);
                }
            }

            if (!string.IsNullOrEmpty(yAxisName))
            {
                float yAxis = Input.GetAxis(yAxisName);
                if (0.5f < Mathf.Abs(yAxis))
                {
                    dir += new Vector3(0f, 0f, yAxis);
                }
            }

            return dir;
        }
    }
}
