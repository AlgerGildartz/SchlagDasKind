Tower_name = "Doom";

function FindTarget( ... )
	for i=1,select("#", ...) do
        local collider = (select(i, ...))
        enemy = collider.gameObject.GetComponent("Enemy")
        enemy.DamageEnemy(10000)
    end
end

