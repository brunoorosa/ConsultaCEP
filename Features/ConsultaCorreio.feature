Feature: ConsultaCorreio
	Consulta de CEP e Codigo de Rastreamento

@ConsultaCorreio
Scenario: ConsultaCEP1
	Given que o CEP informado sera "80700000"
	When solicitar a pesquisa
	Then Retornara que o CEP não existe

Scenario: ConsultaCEP2
	Given que o CEP a ser pesquisado "01013-001"
	When solicitar a pesquisa
	Then Retornara o endereco referente ao CEP

Scenario: ConsultaRastreio
	Given que o codigo de rastreio sera "SS987654321BR"
	When solicitar acompanhamento do objeto 
	Then Retornara que o codigo nao existe
