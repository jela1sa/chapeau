using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Chapeau.Models;

namespace Chapeau.Repositorys
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Order GetOrderByTable(int tableId)
        {
            Order order = null;
            List<OrderItem> orderItems = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        b.bestelling_id,
                        b.tafel_id,
                        b.bediening_id,
                        b.datum_tijd,
                        b.status,
                        b.tijdstip_opgegeven,

                        oi.order_item_id,
                        oi.aantal,

                        mi.item_id,
                        mi.naam,
                        mi.beschrijving,
                        mi.prijs,
                        mi.categorie,
                        mi.btw_tarief

                    FROM BESTELLING b

                    INNER JOIN ORDER_ITEM oi
                        ON b.bestelling_id = oi.bestelling_id

                    INNER JOIN MENU_ITEM mi
                        ON oi.item_id = mi.item_id

                    WHERE b.tafel_id = @tableId
                    AND b.status = 'Open'
                ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tableId", tableId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (order == null)
                            {
                                order = new Order
                                {
                                    OrderId = Convert.ToInt32(reader["bestelling_id"]),
                                    TafelId = Convert.ToInt32(reader["tafel_id"]),
                                    BedieningId = Convert.ToInt32(reader["bediening_id"]),
                                    DatumTijd = Convert.ToDateTime(reader["datum_tijd"]),
                                    Status = reader["status"].ToString(),
                                    TijdstipOpgenomen = TimeOnly.FromDateTime(Convert.ToDateTime(reader["tijdstip_opgegeven"])),
                                    OrderItems = orderItems
                                };
                            }

                            MenuItem menuItem = new MenuItem(
                                Convert.ToInt32(reader["item_id"]),
                                reader["naam"].ToString(),
                                reader["beschrijving"].ToString(),
                                Convert.ToDecimal(reader["prijs"]),
                                reader["categorie"].ToString(),
                                Convert.ToDecimal(reader["btw_tarief"]),
                                null
                            );

                            OrderItem orderItem = new OrderItem()
                            { 
                                OrderItem_id = Convert.ToInt32(reader["order_item_id"]),
                                Aantal = Convert.ToInt32(reader["aantal"]),
                                MenuItem = menuItem
                            };

                            orderItems.Add(orderItem);
                        }
                    }
                }
            }

            return order;
        }
    }
}