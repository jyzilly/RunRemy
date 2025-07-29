using UnityEngine;

namespace ArcadeVP
{
    public class ArcadeVehicleController : MonoBehaviour, IMovement
    {
        public enum groundCheck { rayCast, sphereCaste };
        public enum MovementMode { Velocity, AngularVelocity };
        public MovementMode movementMode;
        public groundCheck GroundCheck;
        public LayerMask drivableSurface;

        public float MaxSpeed, accelaration, turn, gravity = 7f, downforce = 5f;
        [Tooltip("���� ��Ʈ�� ���� ����")]
        public bool AirControl = false;
        [Tooltip("�߰����� ���� �̵� ��. ���� Ŭ���� ���߿��� �̵� ��� �������ϴ�.")]
        public float airControlTranslationForce = 50f;
        [Tooltip("�����̽��ٸ� ������ ���ߴ� ��� �帮��Ʈ�� ��")]
        public bool kartLike = false;
        [Tooltip("kartLike�� true�� ��� �帮��Ʈ�ϴ� ���� ���� ���� �� ���� ȸ���� �� ����")]
        public float driftMultiplier = 1.5f;

        [Header("Speed Limit Settings")]
        [Tooltip("�ִ�ӵ��� �ʰ����� ��, ������ �ִ�ӵ��� �����ϴ� ������ �����մϴ�.")]
        public float decelerationFactor = 10f;

        public Rigidbody rb, carBody;

        [HideInInspector]
        public RaycastHit hit;
        public AnimationCurve frictionCurve;
        public AnimationCurve turnCurve;
        public PhysicsMaterial frictionMaterial;
        [Header("Visuals")]
        public Transform BodyMesh;
        public Transform[] FrontWheels = new Transform[2];
        public Transform[] RearWheels = new Transform[2];
        [HideInInspector]
        public Vector3 carVelocity;

        [Range(0, 10)]
        public float BodyTilt;
        [Header("Audio settings")]
        public AudioSource engineSound;
        [Range(0, 1)]
        public float minPitch;
        [Range(1, 3)]
        public float MaxPitch;
        public AudioSource SkidSound;

        [HideInInspector]
        public float skidWidth;

        public bool parallelMovement = false;

        private float radius, steeringInput, accelerationInput, brakeInput;
        private Vector3 origin;

        public ICarDown carDown;

        public bool inDesert = false;
        public bool InDesert { get { return inDesert; } set { inDesert = value; } }

        public Transform bodyTr;
        //public Transform BodyTr { get { return bodyTr; } set { bodyTr = value; } }

        private void Start()
        {
            radius = rb.GetComponent<SphereCollider>().radius;
            if (movementMode == MovementMode.AngularVelocity)
            {
                Physics.defaultMaxAngularSpeed = MaxSpeed;
            }
            carDown = new RGTCarDownV2();
        }

        private void Update()
        {
            Visuals();
            AudioManager();
        }

        //public void ProvideInputs(float _steeringInput, float _accelarationInput, float _brakeInput)
        //{
        //    steeringInput = _steeringInput;
        //    accelerationInput = _accelarationInput;
        //    brakeInput = _brakeInput;
        //}

        public void AudioManager()
        {
            engineSound.pitch = Mathf.Lerp(minPitch, MaxPitch, Mathf.Abs(carVelocity.z) / MaxSpeed);
            if (Mathf.Abs(carVelocity.x) > 10 && grounded())
            {
                SkidSound.mute = false;
            }
            else
            {
                SkidSound.mute = true;
            }
        }

        public void Move(Vector3 input)
        {
            steeringInput = input.x;
            brakeInput = input.y;
            //accelerationInput = input.z;
            // �ڵ����� ����ؼ� �̵�
            accelerationInput = 1f;


            carVelocity = carBody.transform.InverseTransformDirection(carBody.linearVelocity);

            if (Mathf.Abs(carVelocity.x) > 0)
            {
                //changes friction according to sideways speed of car
                frictionMaterial.dynamicFriction = frictionCurve.Evaluate(Mathf.Abs(carVelocity.x / 100));
                //frictionMaterial.dynamicFriction = Mathf.Clamp(frictionCurve.Evaluate(Mathf.Abs(carVelocity.x / 100)), 0.1f, 1f);
            }


            if (grounded())
            {
                //turnlogic
                float sign = Mathf.Sign(carVelocity.z);
                //float sign = 1f;
                float TurnMultiplyer = turnCurve.Evaluate(carVelocity.magnitude / MaxSpeed);
                if (kartLike && brakeInput > 0.1f) { TurnMultiplyer *= driftMultiplier; } //turn more if drifting

                if (!parallelMovement)
                {
                    //if (accelerationInput > 0.1f || carVelocity.z > 1)
                    //{
                    //    carBody.AddTorque(Vector3.up * steeringInput * sign * turn * 100 * TurnMultiplyer);
                    //}
                    //else if (accelerationInput < -0.1f || carVelocity.z < -1)
                    //{
                    //    carBody.AddTorque(Vector3.up * steeringInput * sign * turn * 100 * TurnMultiplyer);
                    //}
                    // �׻� ȸ������ ���ϵ��� ����
                    carBody.AddTorque(Vector3.up * steeringInput * sign * turn * 100 * TurnMultiplyer);
                }
                else
                {
                    if (accelerationInput > 0.1f || carVelocity.z > 1 || accelerationInput < -0.1f || carVelocity.z < -1)
                    {
                        Vector3 lateralMovement = transform.right * steeringInput * turn * 100;
                        carBody.AddForce(lateralMovement, ForceMode.Acceleration);
                    }
                }



                // mormal brakelogic
                if (!kartLike)
                {
                    if (brakeInput > 0.1f)
                    {
                        rb.constraints = RigidbodyConstraints.FreezeRotationX;
                    }
                    else
                    {
                        rb.constraints = RigidbodyConstraints.None;
                    }
                }

                //accelaration logic

                if (movementMode == MovementMode.AngularVelocity)
                {
                    if (Mathf.Abs(accelerationInput) > 0.1f && brakeInput < 0.1f && !kartLike)
                    {
                        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, carBody.transform.right * accelerationInput * MaxSpeed / radius, accelaration * Time.fixedDeltaTime);
                    }
                    else if (Mathf.Abs(accelerationInput) > 0.1f && kartLike)
                    {
                        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, carBody.transform.right * accelerationInput * MaxSpeed / radius, accelaration * Time.fixedDeltaTime);
                    }
                }
                else if (movementMode == MovementMode.Velocity)
                {
                    if (Mathf.Abs(accelerationInput) > 0.1f && brakeInput < 0.1f && !kartLike)
                    {
                        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, carBody.transform.forward * accelerationInput * MaxSpeed, accelaration / 10 * Time.fixedDeltaTime);
                    }
                    else if (Mathf.Abs(accelerationInput) > 0.1f && kartLike)
                    {
                        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, carBody.transform.forward * accelerationInput * MaxSpeed, accelaration / 10 * Time.fixedDeltaTime);
                    }
                }

                //// Ʈ���Ű� �۵��� ��� carDown�� �����Ű�� �ڵ�
                //if (inDesert && carBody.linearVelocity.magnitude <= 60)
                //{
                //    Debug.Log("������ ����� : " + bodyTr);
                //    carDown.Sink(bodyTr);
                //}

                if(inDesert)
                {
                    if(carBody.linearVelocity.magnitude <= 60)
                    {
                        carDown.Sink(bodyTr);
                    }
                    carDown.Rising(bodyTr);
                }


                // down froce
                rb.AddForce(-transform.up * downforce * rb.mass);

                //body tilt
                carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, hit.normal) * carBody.transform.rotation, 0.12f));
            }
            else
            {
                // if (AirControl)
                // {
                //     //turnlogic
                //     float TurnMultiplyer = turnCurve.Evaluate(carVelocity.magnitude / MaxSpeed);

                //     carBody.AddTorque(Vector3.up * steeringInput * turn * 100 * TurnMultiplyer);
                // }

                // carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, Vector3.up) * carBody.transform.rotation, 0.02f));
                // rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, rb.linearVelocity + Vector3.down * gravity, Time.deltaTime * gravity);

                // ������ �� (AirControl Ȱ��ȭ �� �¿� ȸ�� + �̵� ����)
                if (AirControl)
                {
                    // ���� �¿� ȸ�� ��ũ ����
                    float TurnMultiplyer = turnCurve.Evaluate(carVelocity.magnitude / MaxSpeed);
                    carBody.AddTorque(Vector3.up * steeringInput * turn * airControlTranslationForce * TurnMultiplyer);

                    // ���Ӱ� ���� �̵� �� �߰� (����/���� �� �ణ�� ���� �̵�)
                    Vector3 airTranslationForce = (carBody.transform.right * steeringInput) * airControlTranslationForce;
                    carBody.AddForce(airTranslationForce, ForceMode.Acceleration);

                    //if (!grounded())
                    //{
                    //    // ������ ǥ�鿡 �����ǵ��� �߰� �� ����
                    //    rb.AddForce(-transform.up * gravity * rb.mass);
                    //}
                }

                // ���߿����� ������ õõ�� ���� �ڼ�(����)�� ���ƿ����� ȸ�� ����
                carBody.MoveRotation(Quaternion.Slerp(carBody.rotation,
                    Quaternion.FromToRotation(carBody.transform.up, Vector3.up) * carBody.transform.rotation,
                    0.02f));

                // �߷� ����
                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, rb.linearVelocity + Vector3.down * gravity, Time.fixedDeltaTime * gravity);

                // **�ӵ��� �ʹ� ������ �ִ�ӵ��� ���� ������ ����**
                if (rb.linearVelocity.magnitude > MaxSpeed)
                {
                    rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, rb.linearVelocity.normalized * MaxSpeed, decelerationFactor * Time.fixedDeltaTime);
                }
            }

        }
        public void Visuals()
        {
            //tires
            foreach (Transform FW in FrontWheels)
            {
                FW.localRotation = Quaternion.Slerp(FW.localRotation, Quaternion.Euler(FW.localRotation.eulerAngles.x,
                                   30 * steeringInput, FW.localRotation.eulerAngles.z), 0.7f * Time.deltaTime / Time.fixedDeltaTime);
                FW.GetChild(0).localRotation = rb.transform.localRotation;
            }
            RearWheels[0].localRotation = rb.transform.localRotation;
            RearWheels[1].localRotation = rb.transform.localRotation;

            //Body
            if (carVelocity.z > 1)
            {
                BodyMesh.localRotation = Quaternion.Slerp(BodyMesh.localRotation, Quaternion.Euler(Mathf.Lerp(0, -5, carVelocity.z / MaxSpeed),
                                   BodyMesh.localRotation.eulerAngles.y, BodyTilt * steeringInput), 0.4f * Time.deltaTime / Time.fixedDeltaTime);
            }
            else
            {
                BodyMesh.localRotation = Quaternion.Slerp(BodyMesh.localRotation, Quaternion.Euler(0, 0, 0), 0.4f * Time.deltaTime / Time.fixedDeltaTime);
            }


            if (kartLike)
            {
                if (brakeInput > 0.1f)
                {
                    BodyMesh.parent.localRotation = Quaternion.Slerp(BodyMesh.parent.localRotation,
                    Quaternion.Euler(0, 45 * steeringInput * Mathf.Sign(carVelocity.z), 0),
                    0.1f * Time.deltaTime / Time.fixedDeltaTime);
                }
                else
                {
                    BodyMesh.parent.localRotation = Quaternion.Slerp(BodyMesh.parent.localRotation,
                    Quaternion.Euler(0, 0, 0),
                    0.1f * Time.deltaTime / Time.fixedDeltaTime);
                }

            }

        }

        public bool grounded() //checks for if vehicle is grounded or not
        {
            //origin = rb.position + rb.GetComponent<SphereCollider>().radius * Vector3.up;
            origin = rb.position + transform.up * (rb.GetComponent<SphereCollider>().radius + 0.1f);
            var direction = -transform.up;
            var maxdistance = rb.GetComponent<SphereCollider>().radius + 5f;

            if (GroundCheck == groundCheck.rayCast)
            {
                if (Physics.Raycast(rb.position, Vector3.down, out hit, maxdistance, drivableSurface))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if (GroundCheck == groundCheck.sphereCaste)
            {
                if (Physics.SphereCast(origin, radius * 0.9f, direction, out hit, maxdistance, drivableSurface))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // ������ �±׵� ������Ʈ�� �浹 ��
            if (collision.gameObject.CompareTag("Wall"))
            {
                Vector3 averageNormal = Vector3.zero;
                foreach (ContactPoint contact in collision.contacts)
                {
                    averageNormal += contact.normal;
                }
                averageNormal.Normalize();

                // �浹 ���� �������� �ݵ� ���� �����մϴ�.
                float reboundForce = 100f; // �� ���� �����Ͽ� �ݵ� ������ �����մϴ�.
                rb.AddForce(averageNormal * reboundForce, ForceMode.Impulse);

                // ���� ���� Z����(��, wall.transform.forward)���� ȸ�� ��ũ�� �߰��մϴ�.
                Vector3 wallZDirection = collision.gameObject.transform.forward;
                carBody.transform.rotation = Quaternion.Euler(wallZDirection);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                Vector3 averageNormal = Vector3.zero;
                foreach (ContactPoint contact in collision.contacts)
                {
                    averageNormal += contact.normal;
                }
                averageNormal.Normalize();

                // ������ ���� ����� ���� �ݴ� ����(-����) ������ ������ ����մϴ�.
                float angleDiff = Vector3.SignedAngle(carBody.transform.forward, -averageNormal, Vector3.up);

                // �� ������ ����ϴ� ȸ�� ��ũ�� �߰��Ͽ� �¿� ȸ���� �����մϴ�.
                float rotationTorque = angleDiff * 5f; // ����� �����Ͽ� ȸ�� ������ �����մϴ�.
                carBody.AddTorque(Vector3.up * rotationTorque, ForceMode.Acceleration);
            }
        }

        private void OnDrawGizmos()
        {
            //debug gizmos
            radius = rb.GetComponent<SphereCollider>().radius;
            float width = 0.02f;
            if (!Application.isPlaying)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(rb.transform.position + ((radius + width) * Vector3.down), new Vector3(2 * radius, 2 * width, 4 * radius));
                if (GetComponent<BoxCollider>())
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
                }

            }

        }
    }
}
