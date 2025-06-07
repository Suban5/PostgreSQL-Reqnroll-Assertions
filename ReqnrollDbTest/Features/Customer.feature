Feature: Insert a new customer into the system

	Scenario: Add a new customer and verify it's stored correctly
		Given I cleaned added data in "Customer" table
		Given I have following data in "Customer" table:
			| FirstName | LastName  | Email                        | Phone    | Address         | City         | State | ZipCode | Country |
			| John      | Smith     | john.smith@example.com       | 555-0101 | 123 Main St     | New York     | NY    | 10001   | USA     |
			| Emily     | Johnson   | emily.johnson@example.com    | 555-0102 | 456 Oak Ave     | Los Angeles  | CA    | 90001   | USA     |
			| Michael   | Williams  | michael.williams@example.com | 555-0103 | 789 Pine Rd     | Chicago      | IL    | 60601   | USA     |
			| Sarah     | Brown     | sarah.brown@example.com      | 555-0104 | 321 Elm St      | Houston      | TX    | 77001   | USA     |
			| David     | Jones     | david.jones@example.com      | 555-0105 | 654 Maple Ave   | Phoenix      | AZ    | 85001   | USA     |
			| Jessica   | Garcia    | jessica.garcia@example.com   | 555-0106 | 987 Cedar Ln    | Philadelphia | PA    | 19019   | USA     |
			| Robert    | Miller    | robert.miller@example.com    | 555-0107 | 135 Birch Blvd  | San Antonio  | TX    | 78201   | USA     |
			| Jennifer  | Davis     | jennifer.davis@example.com   | 555-0108 | 246 Walnut Dr   | San Diego    | CA    | 92101   | USA     |
			| Thomas    | Rodriguez | thomas.rodriguez@example.com | 555-0109 | 369 Spruce Ct   | Dallas       | TX    | 75201   | USA     |
			| Lisa      | Martinez  | lisa.martinez@example.com    | 555-0110 | 480 Ash Way     | San Jose     | CA    | 95101   | USA     |
			| James     | Wilson    | james.wilson@example.com     | 555-0111 | 159 Willow Rd   | Austin       | TX    | 73301   | USA     |
			| Elizabeth | Taylor    | elizabeth.taylor@example.com | 555-0112 | 753 Redwood Ave | Jacksonville | FL    | 32099   | USA     |
		When I added the following data in "Customer" table:
			| FirstName | LastName | Email               | Phone        | Address         | City     | State | ZipCode | Country |
			| John      | Doe      | john.doe@test.com   | 123-456-7890 | 123 Main Street | New York | NY    | 99999   | USA     |
			| Ram       | Shrestha | ram.sht123@test.com | null         | Test~~~~~Street |          |       | 99999   |         |
		Then There should exists following data in "Customer" table:
			| FirstName | LastName  | Email                        | Phone        | Address         | City         | State | ZipCode | Country |
			| John      | Smith     | john.smith@example.com       | 555-0101     | 123 Main St     | New York     | NY    | 10001   | USA     |
			| Emily     | Johnson   | emily.johnson@example.com    | 555-0102     | 456 Oak Ave     | Los Angeles  | CA    | 90001   | USA     |
			| Michael   | Williams  | michael.williams@example.com | 555-0103     | 789 Pine Rd     | Chicago      | IL    | 60601   | USA     |
			| Sarah     | Brown     | sarah.brown@example.com      | 555-0104     | 321 Elm St      | Houston      | TX    | 77001   | USA     |
			| David     | Jones     | david.jones@example.com      | 555-0105     | 654 Maple Ave   | Phoenix      | AZ    | 85001   | USA     |
			| Jessica   | Garcia    | jessica.garcia@example.com   | 555-0106     | 987 Cedar Ln    | Philadelphia | PA    | 19019   | USA     |
			| Robert    | Miller    | robert.miller@example.com    | 555-0107     | 135 Birch Blvd  | San Antonio  | TX    | 78201   | USA     |
			| Jennifer  | Davis     | jennifer.davis@example.com   | 555-0108     | 246 Walnut Dr   | San Diego    | CA    | 92101   | USA     |
			| Thomas    | Rodriguez | thomas.rodriguez@example.com | 555-0109     | 369 Spruce Ct   | Dallas       | TX    | 75201   | USA     |
			| Lisa      | Martinez  | lisa.martinez@example.com    | 555-0110     | 480 Ash Way     | San Jose     | CA    | 95101   | USA     |
			| James     | Wilson    | james.wilson@example.com     | 555-0111     | 159 Willow Rd   | Austin       | TX    | 73301   | USA     |
			| Elizabeth | Taylor    | elizabeth.taylor@example.com | 555-0112     | 753 Redwood Ave | Jacksonville | FL    | 32099   | USA     |
			| John      | Doe       | john.doe@test.com            | 123-456-7890 | 123 Main Street | New York     | NY    | 99999   | USA     |
			| Ram       | Shrestha  | ram.sht123@test.com          | null         | Test~~~~~Street |              |       | 99999   |         |
		###Delete row having ZipCode = 99999
		Then I cleaned added data in "Customer" table
		Then There should not exists following data in "Customer" table:
			| FirstName | LastName |
			| John      | Doe      |
			| Ram       | Shrestha |
		Then There should exists following data in "Customer" table:
			| FirstName | LastName  | Email                        | Phone    | Address         | City         | State | ZipCode | Country |
			| John      | Smith     | john.smith@example.com       | 555-0101 | 123 Main St     | New York     | NY    | 10001   | USA     |
			| Emily     | Johnson   | emily.johnson@example.com    | 555-0102 | 456 Oak Ave     | Los Angeles  | CA    | 90001   | USA     |
			| Michael   | Williams  | michael.williams@example.com | 555-0103 | 789 Pine Rd     | Chicago      | IL    | 60601   | USA     |
			| Sarah     | Brown     | sarah.brown@example.com      | 555-0104 | 321 Elm St      | Houston      | TX    | 77001   | USA     |
			| David     | Jones     | david.jones@example.com      | 555-0105 | 654 Maple Ave   | Phoenix      | AZ    | 85001   | USA     |
			| Jessica   | Garcia    | jessica.garcia@example.com   | 555-0106 | 987 Cedar Ln    | Philadelphia | PA    | 19019   | USA     |
			| Robert    | Miller    | robert.miller@example.com    | 555-0107 | 135 Birch Blvd  | San Antonio  | TX    | 78201   | USA     |
			| Jennifer  | Davis     | jennifer.davis@example.com   | 555-0108 | 246 Walnut Dr   | San Diego    | CA    | 92101   | USA     |
			| Thomas    | Rodriguez | thomas.rodriguez@example.com | 555-0109 | 369 Spruce Ct   | Dallas       | TX    | 75201   | USA     |
			| Lisa      | Martinez  | lisa.martinez@example.com    | 555-0110 | 480 Ash Way     | San Jose     | CA    | 95101   | USA     |
			| James     | Wilson    | james.wilson@example.com     | 555-0111 | 159 Willow Rd   | Austin       | TX    | 73301   | USA     |
			| Elizabeth | Taylor    | elizabeth.taylor@example.com | 555-0112 | 753 Redwood Ave | Jacksonville | FL    | 32099   | USA     |
