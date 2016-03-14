mov eax, 10
call @@myfunction
inc eax
push eax
inc eax
pop eax
@@myfunction
mov ebx, eax
push 77
inc eax
pop eax
ret