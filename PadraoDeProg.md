# Padrões de Programação

## Instruções Gerais:
- Código todo em Inglês;
- Comentarios em Inglês ou Português;
- Identação 4 espaços;
- Usar camelCase para tudo menos para class/struct, nesse caso é PascalCase;
- Chaves na linha a baixo;

**Exemplo:**
``` cs

namespace MyNamespace
{
    public class MyClass
    {
        public MyClass()
        {

        }

        int CalculateVar() 
        {
            int var = 0;

            if(var == 0) 
            {
                var = 10;
            }

            return var;
        }
    }
} 
```

## Instruções Para Classes:
- escrever sempre primeiro variaveis, depois propriedades e depois metodos
- Seguir a ordem `private protected public` para modificadores de acesso em metodos e variaveis;
- Sempre usar [SerializeField] para variaveis que vao aparecer no inspector da unity;
- O primeiro metodo publico é sempre o construtor se houver;
- Sempre escrever primeiro metodos da unity (Awake, Update, etc...);
- **Evitar ao máximo** usar variaveis publicas, usar sempre propriedades;
- Escrever sempre variaveis ou metodos com atributos estaticos depois das variaveis ou metodos normais;

**Exemplo:**
``` cs
namespace MyNamespace
{   
    public class EnemyBase
    {        
        [SerializeField] private bool vizibleOnInspector;

        private int hp;
        private int damage;

        protected float speed;

        public bool veryBadPublicVar;

        public bool GoodPublicProperty { get; private set; }

        private void Awake()
        {
            hp = 10;
            damage = 5;
        }

        private void Start()
        {
            
        }

        private void Update()
        {

        }

        private void UpdateHp()
        {
            hp -= 1;
        }

        protected void AddDamagePowerUp(int additionalDamage)
        {
            damage += additionalDamage;
        }

        public EnemyBase()
        {

        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
        }
    }
} 
```

## Design Do Software:
- [Design Patterns](https://github.com/kamranahmedse/design-patterns-for-humans)